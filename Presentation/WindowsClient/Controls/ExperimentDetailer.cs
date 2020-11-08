using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.CQRS;
using MediatR;
using Rems.Application.Common;
using Steema.TeeChart.Styles;
using Rems.Application.Common.Extensions;

namespace WindowsClient.Controls
{
    public delegate Task<T> Querier<T>(IRequest<T> query);

    public partial class ExperimentDetailer : UserControl
    {
        public QueryHandler REMS;

        public ExperimentDetailer()
        {
            InitializeComponent();

            operationsBox.SelectedIndex = 0;            

            experimentsTree.AfterSelect += OnExperimentNodeChanged;
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        public async Task RefreshContent()
        {
            traitChart.REMS = REMS;

            experimentsTree.Nodes.Clear();

            var exps = await new ExperimentsQuery().Send(REMS);

            foreach (var exp in exps)
            {
                TreeNode eNode = new TreeNode(exp.Value) { Tag = new NodeTag() { ID = exp.Key, Type = TagType.Experiment } };

                var treatments = await (new TreatmentsQuery() { ExperimentId = exp.Key }).Send(REMS);

                foreach (var treatment in treatments)
                {
                    TreeNode tNode = new TreeNode(treatment.Value)
                    {
                        Tag = new NodeTag() { ID = treatment.Key, Type = TagType.Treatment }
                    };
                    tNode.Nodes.Add(new TreeNode("All") { Tag = new NodeTag() { Type = TagType.Empty } });

                    var plots = await (new PlotsQuery() { TreatmentId = treatment.Key }).Send(REMS);

                    tNode.Nodes.AddRange(plots.Select(p =>
                    {
                        var tag = new NodeTag() { ID = p.Key, Type = TagType.Plot };
                        return new TreeNode(p.Value) { Tag = tag };
                    }).ToArray());
                    eNode.Nodes.Add(tNode);
                }

                experimentsTree.Nodes.Add(eNode);
            }
            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();

            traitChart.LoadTraitsBox();
        }

        private void OnExperimentNodeChanged(object sender, EventArgs e)
        {
            traitChart.Node = experimentsTree.SelectedNode;

            RefreshSummary();
            RefreshOperationsData();
        }

        private void OnOperationSelection(object sender, EventArgs e)
        {
            RefreshOperationsData();
        }

        private async void RefreshSummary()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            if (node.Tag is NodeTag tag && tag.Type == TagType.Experiment)
            {
                var query = new ExperimentSummary() { ExperimentId = tag.ID };

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

                var sowing = await (new SowingSummary() { ExperimentId = tag.ID }).Send(REMS);
                sowingMethodBox.Content = sowing["Method"];
                sowingDateBox.Content = sowing["Date"];
                sowingDepthBox.Content = sowing["Depth"];
                sowingRowBox.Content = sowing["Row"];
                sowingPopBox.Content = sowing["Pop"];

                var design = new DesignsTableQuery() { ExperimentId = tag.ID };
                designData.DataSource = await design.Send(REMS);
            }
        }

        private async void RefreshOperationsData()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            var tag = (NodeTag)node.Tag;
            if (tag.Type is TagType.Experiment) return;

            IRequest<SeriesData> query;

            string item = operationsBox.SelectedItem?.ToString();
            if (item == "Irrigations")
                query = new IrrigationDataQuery() { TreatmentId = tag.ID };
            else if (item == "Fertilizations")
                query = new FertilizationDataQuery() { TreatmentId = tag.ID };
            else if (item == "Tillages")
                query = new TillagesDataQuery() { TreatmentId = tag.ID };
            else
                return;

            var data = await query.Send(REMS);

            operationsChart.Series.Clear();
            operationsChart.Text = item;

            // Data
            Bar bar = new Bar();
            bar.CustomBarWidth = 8;
            bar.Marks.Visible = false;

            bar.Add(data.X, data.Y);

            // X-Axis
            bar.XValues.DateTime = true;
            //operationsChart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
            //operationsChart.Axes.Bottom.Labels.Angle = 90;
            operationsChart.Axes.Bottom.Title.Text = data.XLabel;
            //operationsChart.Axes.Bottom.Ticks.Visible = true;

            // Y-Axis
            operationsChart.Axes.Left.Title.Text = data.YLabel;

            // Legend            
            bar.Legend.Visible = false;


            operationsChart.Series.Add(bar);
            operationsChart.Refresh();
        }

        
    }
}
