using MediatR;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /// <summary>
        /// Occurs when data is requested from the mediator
        /// </summary>
        public event Func<object, Task<object>> Query;
        
        /// <summary>
        /// Safely handles a query
        /// </summary>
        /// <typeparam name="T">The type of data requested</typeparam>
        /// <param name="query">The request object</param>
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T) await Query(query);        

        public ExperimentDetailer()
        {
            InitializeComponent();          

            experimentsTree.AfterSelect += OnNodeSelected;

            summariser.Query += (o) => Query?.Invoke(o);
            operations.Query += (o) => Query?.Invoke(o);
            traitChart.Query += (o) => Query?.Invoke(o);
            soilsChart.Query += (o) => Query?.Invoke(o);
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        public async void LoadNodes()
        {
            experimentsTree.Nodes.Clear();

            var exps = await InvokeQuery(new ExperimentsQuery());

            foreach (var exp in exps)
            {
                ENode eNode = new ENode(exp.Value) { EID = exp.Key };

                var treats = await InvokeQuery(new TreatmentsQuery{ ExperimentId = exp.Key });

                foreach (var treat in treats)
                {
                    TNode tNode = new TNode(treat.Value) { EID = exp.Key, TID = treat.Key };

                    tNode.Nodes.Add(new TNode("All") { EID = exp.Key, TID = treat.Key });

                    var plots = await InvokeQuery(new PlotsQuery{ TreatmentId = treat.Key });

                    tNode.Nodes.AddRange(plots.Select(p => 
                        new PNode(p.Value) { EID = exp.Key, TID = treat.Key, PID = p.Key}
                    ).ToArray());
                    eNode.Nodes.Add(tNode);
                }

                experimentsTree.Nodes.Add(eNode);
            }
            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();
        }

        /// <summary>
        /// Calls a function dependent on the type of node selected in the tree
        /// </summary>
        private async void OnNodeSelected(object sender, EventArgs e)
        {
            var node = experimentsTree.SelectedNode;

            if (node is PNode plot)
                await PlotSelected(plot);
            else if (node is TNode treatment)
                await TreatmentSelected(treatment);
            else if (node is ENode experiment)
                await RefreshSummary(experiment.EID);            
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
            designData.DataSource = await InvokeQuery(design);
        }
    }
}
