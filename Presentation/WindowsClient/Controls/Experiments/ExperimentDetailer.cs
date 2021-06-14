using MediatR;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of experiment data
    /// </summary>
    public partial class ExperimentDetailer : UserControl
    {
        #region Nodes
        /// <summary> Experiment node </summary>
        private class ENode : TreeNode
        {
            public ENode(string name) : base(name) { }

            /// <summary> Experiment ID </summary>
            public int EID { get; set; }
        }

        /// <summary> Treatment node </summary>
        private class TNode : ENode
        {
            public TNode(string name) : base(name) { }

            /// <summary> Treatment ID </summary>
            public int TID { get; set; }
        }

        /// <summary> Plot node </summary>
        private class PNode : TNode
        {
            public PNode(string name) : base(name) { }

            /// <summary> Plot ID </summary>
            public int PID { get; set; }
        }
        #endregion

        public ExperimentDetailer()
        {
            InitializeComponent();          
            
            experimentsTree.AfterSelect += OnNodeSelected;
            Load += async (s, e) => await LoadNodes(); 
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        public async Task LoadTreeView()
        {
            experimentsTree.Nodes.Clear();

            var exps = await QueryManager.Request(new ExperimentsQuery());

            foreach (var exp in exps)
            {
                var node = await AddTreatmentNodes(exp.Key, exp.Value);
                                
                node.Nodes.Add(new ControlNode<ExperimentSummariser>("Design"));
                node.Nodes.Add(new ControlNode<OperationsChart>("Operations"));
                node.Nodes.Add(new ControlNode<TraitChart>("Crop"));
                node.Nodes.Add(new ControlNode<SoilChart>("Soil"));
            }

            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();
        }

        private async Task<ENode> AddTreatmentNodes(int id, string name)
        {
            ENode eNode = new ENode(name) { EID = id };

            var treats = await QueryManager.Request(new TreatmentsQuery { ExperimentId = id });

            foreach (var treat in treats)
            {
                TNode tNode = new TNode(treat.Value) { EID = id, TID = treat.Key };

                tNode.Nodes.Add(new TNode("All") { EID = id, TID = treat.Key });

                var plots = await QueryManager.Request(new PlotsQuery { TreatmentId = treat.Key });

                tNode.Nodes.AddRange(plots.Select(p =>
                    new PNode(p.Value) { EID = id, TID = treat.Key, PID = p.Key }
                ).ToArray());
                eNode.Nodes.Add(tNode);
            }

            experimentsTree.Nodes.Add(eNode);

            return eNode;
        }

        /// <summary>
        /// Calls a function dependent on the type of node selected in the tree
        /// </summary>
        private async void OnNodeSelected(object sender, EventArgs e)
        {
            var node = experimentsTree.SelectedNode;

            try
            {
                if (node is PNode plot)
                    await PlotSelected(plot);
                else if (node is TNode treatment)
                    await TreatmentSelected(treatment);
                else if (node is ENode experiment)
                    await RefreshSummary(experiment.EID);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// Updates the charts for the selected treatment
        /// </summary>
        /// <param name="node">The treatment node</param>
        private async Task TreatmentSelected(TNode node)
        {
            await operations.UpdateData(node.TID);

            await traitChart.LoadTraitsBox(node.TID);
            await soilsChart.LoadBoxes(node.TID);

            if (node.Text == "All")
            {
                await traitChart.UpdateAll(node.TID, node);
                await soilsChart.UpdateAll(node.TID, node);
            }
            else
            {                
                await traitChart.UpdateMean(node.TID, node);                
                await soilsChart.UpdateMean(node.TID, node);
            }

            await RefreshSummary(node.EID);
        }

        /// <summary>
        /// Updates the charts for the selected plot
        /// </summary>
        /// <param name="node">The plot node</param>
        private async Task PlotSelected(PNode node)
        {
            await traitChart.LoadTraitsBox(node.TID);
            await soilsChart.LoadBoxes(node.TID);

            await operations.UpdateData(node.TID);
            await traitChart.UpdateSingle(node.PID, node);
            await soilsChart.UpdateSingle(node.PID, node);
            await RefreshSummary(node.EID);
        }

        private int expid = -1;
        /// <summary>
        /// Loads the summary data for the current experiment
        /// </summary>
        /// <param name="id"></param>
        private async Task RefreshSummary(int id)
        {
            if (id == expid)
                return;
            else
                expid = id;

            await summariser.GetSummary(id);

            var design = new DesignsTableQuery() { ExperimentId = id };
            //designData.DataSource = await QueryManager.Request(design);
        }
    }
}
