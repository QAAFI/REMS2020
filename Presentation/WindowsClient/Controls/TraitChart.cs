using System;
using System.Windows.Forms;

using Steema.TeeChart.Styles;

using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

namespace WindowsClient.Controls
{
    public partial class TraitChart : UserControl
    {
        //public Axis XAxis { get; }
        //public Axis YAxis { get; }

        private TreeNode node;
        public TreeNode Node 
        {
            get { return node; }
            set
            {
                node = value;
                if (value is null)
                    chart.Series.Clear();
                else
                {
                    RefreshSoilDataDates();
                }
            }
        }

        public QueryHandler REMS;

        private DateTime[] dates = new DateTime[1];
        private int date = 0;

        public TraitChart()
        {
            InitializeComponent();

            chart.Axes.Left.AutomaticMinimum = false;
            chart.Axes.Left.Minimum = 0;
            chart.Text = "Crop Traits";

            //traitTypeBox.SelectedValueChanged += OnTraitChanged;
            traitsBox.SelectedIndexChanged += OnItemCheck;
        }

        private void OnItemCheck(object sender, EventArgs e) => throw new NotImplementedException();

        private void OnTraitChanged(object sender, EventArgs args) => RefreshTraitsList();

        private void PlotSeries(SeriesData series)
        {
            if (series is null) return;
            if (series.X.Length == 0) return;

            chart.Axes.Left.Title.Text = series.YLabel;

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

        // On Node changing
        public async void RefreshSoilDataDates()
        {
            date = 0;
            
            var tag = (NodeTag)node.Tag;
            var query = new SoilLayerDatesQuery();
            switch (tag.Type)
            {
                case TagType.Treatment:
                    query.TreatmentId = tag.ID;                    
                    break;

                case TagType.Plot:
                    query.TreatmentId = ((NodeTag)node.Parent.Tag).ID;
                    break;

                case TagType.Experiment:                    
                    break;

                default:
                    if (node.Text == "All")
                        query.TreatmentId = ((NodeTag)node.Parent.Tag).ID;
                    break;
            }
            dates = await query.Send(REMS);

            if (dates.Length < 1) dates = new DateTime[1];
        }

        public async void LoadTraitsBox()
        {
            //traitTypeBox.Items.Clear();

            // Load the trait type box
            var types = await new TraitTypesQuery().Send(REMS);

            if (types.Length == 0) return;

            //traitTypeBox.Items.AddRange(types);
            //traitTypeBox.SelectedIndex = 0;            
        }

        public async void RefreshTraitsList()
        {
            traitsBox.Items.Clear();
            //var query = new TraitsByTypeQuery() { Type = traitTypeBox.SelectedItem.ToString() };
            //var traits = await query.Send(REMS);
            //traitsBox.Items.AddRange(traits);
            traitsBox.Refresh();

            //RefreshChart();
        }

        private async void SetAxisBounds(string trait)
        {
            //var query = new PlotDataTraitBoundsQuery() { TraitName = trait };
            //var bounds = await BoundsQuery.Invoke(query);

            //if (bounds.YMin < YAxis.Minimum) YAxis.Minimum = bounds.YMin - bounds.YMin / 10;
            //if (bounds.YMax > YAxis.Maximum) YAxis.Maximum = bounds.YMax + bounds.YMax / 10;
        }

        public async void RefreshCropData()
        {
            chart.Series.Clear();
            chart.Text = "Crop Traits";
            chart.Axes.Left.Inverted = false;
            chart.Axes.Bottom.AutomaticMinimum = true;

            foreach (string trait in traitsBox.CheckedItems)
            {
                SetAxisBounds(trait);

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

        }

        public async void RefreshSoilData()
        {
            chart.Series.Clear();

            chart.Axes.Left.Inverted = true;
            chart.Axes.Bottom.Minimum = 0;
            chart.Axes.Bottom.AutomaticMinimum = false;

            chart.Text = "Soil traits";

            var query = new TraitDataOnDateQuery()
            {
                Date = dates[date]
            };

            foreach (string trait in traitsBox.CheckedItems)
            {
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
                            Date = dates[date],
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

        private void LeftClicked(object sender, EventArgs e)
        {
            date--;
            if (date < 0) date = dates.Length - 1;

            RefreshSoilData();
        }

        private void RightClicked(object sender, EventArgs e)
        {
            date++;
            if (date > dates.Length - 1) date = 0;

            RefreshSoilData();
        }
    }
}
