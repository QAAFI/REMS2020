using System;
using System.Windows.Forms;

using Steema.TeeChart.Styles;

using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System.Threading.Tasks;

namespace WindowsClient.Controls
{
    public partial class TraitChart : UserControl
    {
        //public Axis XAxis { get; }
        //public Axis YAxis { get; }

        private TreeNode node;
        public TreeNode Node 
        {
            get => node;
            set
            {
                node = value;
                if (value is null)
                    chart.Series.Clear();
                else
                    LoadTraitsBox();
            }
        }

        public QueryHandler REMS;

        public TraitChart()
        {
            InitializeComponent();

            chart.Chart.Panel.MarginUnits = Steema.TeeChart.PanelMarginUnits.Pixels;
            chart.Axes.Left.AutomaticMinimum = false;
            chart.Axes.Left.Minimum = 0;
            chart.Text = "Crop Traits";

            traitsBox.SelectedIndexChanged += OnTraitSelected;
        }

        private async void OnTraitSelected(object sender, EventArgs e)
        {
            string trait = traitsBox.SelectedItem.ToString();
            string type = await (new TraitTypeQuery() { Name = trait }).Send(REMS);

            if (type == "Soil")
                RefreshSoilLayerData();
            else
                RefreshCropData();
        }

        private void PlotSeries(SeriesData series)
        {
            if (series is null) return;
            if (series.X.Length == 0) return;

            chart.Text = series.Title;
            chart.Axes.Left.Title.Text = series.YLabel;
            chart.Axes.Bottom.Title.Text = series.XLabel;

            Points points = new Points();
            points.Legend.Text = series.Title;
            points.Legend.Visible = false;

            Line line = new Line();
            line.Legend.Visible = false;            

            if (series.X.GetValue(0) is DateTime)
            {
                points.XValues.DateTime = true;
                line.XValues.DateTime = true;

                chart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
                chart.Axes.Bottom.Labels.Angle = 60;
                chart.Axes.Bottom.Ticks.Visible = true;
            }

            line.Add(series.X, series.Y);
            points.Add(series.X, series.Y);

            chart.Series.Add(line);
            chart.Series.Add(points);

            line.Color = points.Color;
        }

        public async void LoadTraitsBox()
        {
            traitsBox.Items.Clear();

            int id = GetNodeId();
            if (id == -1) return;

            // Load the trait type box
            var types = await (new TreatmentTraitsQuery() { TreatmentId = id }).Send(REMS);

            if (types.Length == 0) return;

            traitsBox.Items.AddRange(types);            
        }

        private int GetNodeId()
        {
            var tag = (NodeTag)node.Tag;
            switch (tag.Type)
            {
                case TagType.Treatment:
                    return tag.ID;

                case TagType.Plot:
                    return ((NodeTag)node.Parent.Tag).ID;

                case TagType.Experiment:
                    return -1;

                default:
                    if (node.Text == "All")
                        return ((NodeTag)node.Parent.Tag).ID;
                    return -1;
            }
        }

        public async void RefreshCropData()
        {
            listSplitter.Panel2Collapsed = true;
            chart.Chart.Panel.MarginBottom = 50;

            chart.Series.Clear();
            chart.Text = "Crop Traits";
            chart.Axes.Left.Inverted = false;
            chart.Axes.Bottom.AutomaticMinimum = true;

            string trait = traitsBox.SelectedItem.ToString();
            var query = new PlotDataByTraitQuery() { TraitName = trait };

            var tag = (NodeTag)node.Tag;
            switch (tag.Type)
            {
                case TagType.Experiment:
                    // TODO: Decide what to implement at experiment level
                    break;

                case TagType.Treatment:
                    var mean = new MeanTreatmentDataByTraitQuery()
                    {
                        TraitName = trait,
                        TreatmentId = tag.ID
                    };
                    PlotSeries(await mean.Send(REMS));
                    break;

                case TagType.Plot:
                    query.PlotId = tag.ID;
                    PlotSeries(await query.Send(REMS));
                    break;

                default:
                    if (node.Text == "All")
                    {
                        foreach (TreeNode plot in node.Parent.Nodes)
                        {
                            if (plot.Tag is NodeTag t && t.Type == TagType.Plot)
                            {
                                query.PlotId = t.ID;
                                PlotSeries(await query.Send(REMS));
                            }
                        }
                    }
                    break;
            }
        }

        public async void RefreshSoilLayerData()
        {
            listSplitter.Panel2Collapsed = false;

            chart.Series.Clear();

            chart.Axes.Left.Inverted = true;
            chart.Axes.Bottom.Minimum = 0;
            chart.Axes.Bottom.AutomaticMinimum = false;
            
            chart.Chart.Panel.MarginBottom = 20;

            await LoadDatesBox();
        }

        private async Task LoadDatesBox()
        {
            datesBox.Items.Clear();

            int id = GetNodeId();
            if (id == -1) return;

            var query = new SoilLayerDatesQuery() { TreatmentId = id };
            var dates = await query.Send(REMS);
            
            foreach (var date in dates)
                datesBox.Items.Add(date);
        }

        private async void OnDateSelected(object sender, EventArgs e)
        {
            chart.Series.Clear();

            var date = Convert.ToDateTime(datesBox.SelectedItem);            

            string trait = traitsBox.SelectedItem.ToString();
            chart.Text = trait;            

            var query = new TraitDataOnDateQuery()
            {
                Date = date
            };
            query.TraitName = trait;

            var tag = (NodeTag)node.Tag;
            switch (tag.Type)
            {
                case TagType.Experiment:
                    // TODO: Decide what to implement at experiment level
                    break;

                case TagType.Treatment:
                    var mean = new MeanDataQuery()
                    {
                        Date = date,
                        TraitName = trait,
                        TreatmentId = tag.ID
                    };
                    PlotSeries(await mean.Send(REMS));
                    break;

                case TagType.Plot:
                    query.PlotId = tag.ID;
                    PlotSeries(await query.Send(REMS));
                    break;

                default:
                    if (node.Text == "All")
                    {
                        foreach (TreeNode plot in node.Parent.Nodes)
                        {
                            if (plot.Tag is NodeTag t && t.Type == TagType.Plot)
                            {
                                query.PlotId = t.ID;
                                PlotSeries(await query.Send(REMS));
                            }
                        }
                    }
                    break;
            }
        }
    }
}
