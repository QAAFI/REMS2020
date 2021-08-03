using Rems.Application.CQRS;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public interface ITreatmentControl
    {
        /// <summary>
        /// The current treatment ID
        /// </summary>
        int Treatment { get; set; }

        Task Initialise(int experiment);

        Task LoadTreatment(int treatment);
    }

    public class ExperimentNode : TreeNode
    {
        public int ID { get; set; }

        private ControlNode<OperationsChart> operations = new("Operations");
        private ControlNode<CropChart> crop = new("Crop");
        private ControlNode<SoilChart> soil = new("Soil");
        private ControlNode<SoilLayerChart> layers = new("Soil layers");

        public ExperimentNode(string text) : base(text)
        {
            Nodes.Add(operations);
            Nodes.Add(crop);
            Nodes.Add(soil);
            Nodes.Add(layers);
        }

        public Task<TabControl> GetSelectedControl()
        {
            if (operations.IsSelected)
                return operations.Create(ID);

            if (crop.IsSelected)
                return crop.Create(ID);

            if (soil.IsSelected)
                return soil.Create(ID);

            if (layers.IsSelected)
                return layers.Create(ID);

            return null;
        }
    }

    public class ControlNode<TControl> : TreeNode where TControl : UserControl, ITreatmentControl, new()
    {
        public ControlNode(string text) : base(text)
        { }

        public async Task<TabControl> Create(int id)
        {
            var tabs = new TabControl { Dock = DockStyle.Fill };

            var treatments = await QueryManager.Request(new TreatmentsQuery { ExperimentId = id });

            foreach(var treat in treatments)
            {
                var control = new TControl();

                await control.LoadTreatment(treat.Key).TryRun();
                control.Dock = DockStyle.Fill;

                var page = new TabPage(treat.Value);
                page.Controls.Add(control);
                tabs.TabPages.Add(page);
            }            

            return tabs;
        }
    }
}
