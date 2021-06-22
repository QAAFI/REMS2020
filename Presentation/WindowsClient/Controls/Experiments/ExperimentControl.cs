using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        Task LoadTreatment(int id);
    }

    public class ExperimentNode : TreeNode
    {
        public int ID { get; set; }

        //private ControlNode<ExperimentSummariser> design = new("Design");
        private ControlNode<OperationsChart> operations = new("Operations");
        private ControlNode<TraitChart> crop = new("Crop");
        private ControlNode<SoilChart> soil = new("Soil");

        public ExperimentNode(string text) : base(text)
        {
            //Nodes.Add(design);
            Nodes.Add(operations);
            Nodes.Add(crop);
            Nodes.Add(soil);
        }

        public Task<TabControl> GetSelectedControl()
        {
            //if (design.IsSelected)
            //    return design.Create(ID);

            if (operations.IsSelected)
                return operations.Create(ID);

            if (crop.IsSelected)
                return crop.Create(ID);

            if (soil.IsSelected)
                return soil.Create(ID);

            return null;
        }
    }

    public class ControlNode<TControl> : TreeNode where TControl : UserControl, ITreatmentControl, new()
    {
        public ControlNode(string text) : base(text)
        { }

        public async Task<TabControl> Create(int id)
        {
            var tabs = new TabControl();

            var treatments = await QueryManager.Request(new TreatmentsQuery { ExperimentId = id });

            foreach(var treat in treatments)
            {
                var control = new TControl();

                await control.LoadTreatment(treat.Key);
                control.Dock = DockStyle.Fill;

                var page = new TabPage(treat.Value);
                page.Controls.Add(control);
                tabs.TabPages.Add(page);
            }            

            return tabs;
        }
    }
}
