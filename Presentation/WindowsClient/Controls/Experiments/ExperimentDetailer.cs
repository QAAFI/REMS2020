using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using MediatR;
using Rems.Application.CQRS;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;

namespace WindowsClient.Controls
{
    

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

        public event QueryHandler REMS;

        public ExperimentDetailer()
        {
            InitializeComponent();          

            experimentsTree.AfterSelect += OnExperimentNodeChanged;

            operations.DataRequested += (o, token) => REMS?.Invoke(o);
            traitChart.DataRequested += (o, token) => REMS?.Invoke(o);
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        public async void LoadNodes()
        {
            experimentsTree.Nodes.Clear();

            var exps = await REMS.Send(new ExperimentsQuery());

            foreach (var exp in exps)
            {
                ENode eNode = new ENode(exp.Value) { EID = exp.Key };

                var treats = await REMS.Send(new TreatmentsQuery{ ExperimentId = exp.Key });

                foreach (var treat in treats)
                {
                    TNode tNode = new TNode(treat.Value) { EID = exp.Key, TID = treat.Key };

                    tNode.Nodes.Add(new TNode("All") { EID = exp.Key, TID = treat.Key });

                    var plots = await REMS.Send(new PlotsQuery{ TreatmentId = treat.Key });

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

        private async void OnExperimentNodeChanged(object sender, EventArgs e)
        {
            var node = experimentsTree.SelectedNode;

            if (node is PNode plot)
                await PlotSelected(plot);
            else if (node is TNode treatment)
                await TreatmentSelected(treatment);
            else if (node is ENode experiment)
                await ExperimentSelected(experiment);            
        }

        private async Task ExperimentSelected(ENode node)
        {
            await RefreshSummary(node.EID);
        }

        private async Task TreatmentSelected(TNode node)
        {
            await operations.UpdateData(node.TID);

            if (node.Text == "All")
                await traitChart.UpdateAll(node.TID, node);
            else
            {
                await traitChart.LoadTraitsBox(node.TID);
                await traitChart.UpdateMean(node.TID, node);
            }

            await RefreshSummary(node.EID);
        }

        private async Task PlotSelected(PNode node)
        {
            await operations.UpdateData(node.TID);
            await traitChart.UpdateSingle(node.PID, node);

            await RefreshSummary(node.EID);
        }

        private int expid = -1;
        private async Task RefreshSummary(int id)
        {
            if (id == expid)
                return;
            else
                expid = id;

            var query = new ExperimentSummary() { ExperimentId = id };

            var experiment = await query.Send(REMS);

            descriptionBox.Text = experiment["Description"];
            designBox.Text = experiment["Design"];
            cropBox.Content = experiment["Crop"];
            fieldBox.Content = experiment["Field"];
            metBox.Content = experiment["Met"];
            repsBox.Content = experiment["Reps"];
            ratingBox.Content = experiment["Rating"];
            startBox.Content = experiment["Start"];
            endBox.Content = experiment["End"];

            var items = experiment["List"].Split('\n');
            researchersBox.Items.Clear();
            researchersBox.Items.AddRange(items);

            notesBox.Text = experiment["Notes"];

            var sowing = await REMS.Send(new SowingSummary() { ExperimentId = id });
            sowingMethodBox.Content = sowing["Method"];
            sowingDateBox.Content = sowing["Date"];
            sowingDepthBox.Content = sowing["Depth"];
            sowingRowBox.Content = sowing["Row"];
            sowingPopBox.Content = sowing["Pop"];

            var design = new DesignsTableQuery() { ExperimentId = id };
            designData.DataSource = await REMS.Send(design);
            
        }        
    }
}
