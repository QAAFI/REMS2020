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
using Steema.TeeChart;
using static Steema.TeeChart.Axis;
using System.Drawing;

namespace WindowsClient.Controls
{
    public delegate Task<T> Querier<T>(IRequest<T> query);

    public partial class ExperimentDetailer : UserControl
    {
        public QueryHandler REMS;

        public ExperimentDetailer()
        {
            InitializeComponent();          

            experimentsTree.AfterSelect += OnExperimentNodeChanged;
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        public async void RefreshContent(object sender, EventArgs e)
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
            operationsChart.Text = "Operations";
            var chart = operationsChart.Chart;
            chart.Panel.MarginUnits = PanelMarginUnits.Pixels;
            chart.Panel.MarginLeft = 70;
            chart.Panel.MarginRight = 30;
            chart.Panel.MarginBottom = 90;            

            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            var tag = (NodeTag)node.Tag;
            if (tag.Type is TagType.Experiment) return;

            var iData = await (new IrrigationDataQuery() { TreatmentId = tag.ID }).Send(REMS);
            var fData = await (new FertilizationDataQuery() { TreatmentId = tag.ID }).Send(REMS);
            var tData = await (new TillagesDataQuery() { TreatmentId = tag.ID }).Send(REMS);

            chart.Series.Clear();
            chart.Axes.Custom.Clear();

            var pen = new AxisLinePen(chart)
            {
                Visible = true,
                Color = Color.Black,
                Width = 2
            };
            chart.Axes.Right.AxisPen = pen;
            chart.Axes.Right.Visible = true;

            var x = new Axis(chart)
            {
                Title = new AxisTitle() { Text = "Date" },
                AutomaticMaximum = true,
                AutomaticMinimum = true,
                Horizontal = true,
            };
            x.Labels.DateTimeFormat = "MMM-dd";
            x.Labels.Angle = 60;
            x.Ticks.Visible = true;
            x.MinorGrid.Visible = true;
            x.MinorGrid.Color = Color.LightGray;
            x.Grid.Visible = true;

            var y = new Axis(chart)
            {
                AxisPen = pen
            };

            chart.Axes.Custom.Add(x);
            chart.Axes.Custom.Add(y);

            chart.Series.Add(CreateBar(iData, x, 0));
            chart.Series.Add(CreateBar(fData, x, 1));
            chart.Series.Add(CreateBar(tData, x, 2));           

            chart.Draw();
        }

        private Bar CreateBar(SeriesData data, Axis x, int pos)
        {
            var chart = operationsChart.Chart;

            int margin = 5 * pos;
            int start = 30 * pos + margin;
            int end = start + 30;

            var title = new AxisTitle() 
            { 
                Text = data.Title + " " + data.YLabel,
                Angle = 90          
            };
            title.Font.Size = 8;

            int increment = Convert.ToInt32(Math.Floor(data.Y.Cast<double>().Max() / 30)) * 10;
            var y = new Axis(chart)
            {
                Title = title,
                StartPosition = start,
                EndPosition = end,
                MinorTickCount = 1,
                Increment = increment
            };
            y.MinorGrid.Visible = true;
            y.MinorGrid.Color = Color.LightGray;

            var b = new Axis(chart)
            {                
                AxisPen = new AxisLinePen(chart) { Visible = true, Width = 2, Color = Color.Black },
                Horizontal = true,
                Visible = true,
                RelativePosition = start
            };            

            chart.Axes.Custom.Add(y);
            chart.Axes.Custom.Add(b);

            // Data
            Bar bar = new Bar()
            {
                CustomBarWidth = 4,
                CustomHorizAxis = x,
                CustomVertAxis = y,
                Title = data.Title
            };
            bar.Marks.Visible = false;                        

            bar.Add(data.X, data.Y);

            // X-Axis
            bar.XValues.DateTime = true;
           
            // Legend            
            bar.Legend.Visible = false;

            return bar;
        }
    }
}
