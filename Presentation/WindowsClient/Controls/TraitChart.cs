using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Steema.TeeChart;
using Rems.Application.Common;
using Steema.TeeChart.Styles;
using MediatR;
using Rems.Application.CQRS;
using Rems.Application.Common.Interfaces;
using Steema.TeeChart.Drawing;

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
                    RefreshChart();
            }
        }

        public delegate Task<T> QueryHandler<T>(IRequest<T> query);
        public event QueryHandler<SeriesData> SeriesQuery;
        public event QueryHandler<string[]> StringsQuery;
        public event QueryHandler<DateTime[]> DatesQuery;
        public event QueryHandler<PlotDataBounds> BoundsQuery;

        private DateTime[] dates;
        private int date = 0;

        public TraitChart()
        {
            InitializeComponent();

            //XAxis = new Axis(chart.Chart)
            //{
            //    Horizontal = true,
            //    AutomaticMaximum = true,
            //    AutomaticMinimum = true
            //};

            //chart.Axes.Custom.Add(XAxis);
            //chart.Axes.Custom.Add(YAxis);

            chart.Axes.Left.AutomaticMinimum = false;
            chart.Axes.Left.Minimum = 0;

            //chart.Axes.Bottom.Grid.Visible = true;
            //chart.Axes.Bottom.Grid.DrawEvery = 1;

            chart.Text = "Crop Traits";

            traitTypeBox.SelectedValueChanged += OnTraitChanged;
            traitsBox.SelectedIndexChanged += OnItemCheck;
        }

        private void OnItemCheck(object sender, EventArgs e) => RefreshChart();

        private void OnTraitChanged(object sender, EventArgs args) => RefreshTraitsList();

        public void PlotSeries(SeriesData series)
        {
            if (series is null) return;
            if (series.X.Length == 0) return;

            chart.Axes.Left.Title.Text = series.YLabel;

            Points points = new Points();
            //{
            //    CustomVertAxis = YAxis,
            //    //CustomHorizAxis = XAxis
            //};
            points.Legend.Text = series.Name;
            points.Legend.Visible = false;

            Line line = new Line();
            //{
            //    CustomVertAxis = YAxis,
            //    //CustomHorizAxis = XAxis
            //};
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

            //chart.Refresh();
        }

        private void RefreshChart()
        {
            if (node is null) return;

            leftBtn.Visible = false;
            rightBtn.Visible = false;

            if (traitTypeBox.Text == "Crop") RefreshCropData();
            if (traitTypeBox.Text == "SoilLayer") RefreshSoilControl();
        }

        private async void RefreshSoilControl()
        {
            if (!await RefreshSoilDataDates()) return;

            leftBtn.Visible = true;
            rightBtn.Visible = true;

            RefreshSoilData();
        }

        public async Task<bool> RefreshSoilDataDates()
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

                default:
                    return false;
            }
            dates = await Task.Run(() => DatesQuery.Invoke(query));

            if (dates.Length > 0)
                return true;
            else
                return false;
        }

        public async void LoadTraitsBox()
        {
            traitTypeBox.Items.Clear();

            // Load the trait type box
            var types = await StringsQuery?.Invoke(new TraitTypesQuery());

            if (types.Length == 0) return;

            traitTypeBox.Items.AddRange(types);
            traitTypeBox.SelectedIndex = 0;
        }

        public async void RefreshTraitsList()
        {
            traitsBox.Items.Clear();
            var query = new TraitsByTypeQuery() { Type = traitTypeBox.SelectedItem.ToString() };
            var traits = await StringsQuery.Invoke(query);
            traitsBox.Items.AddRange(traits);
            traitsBox.Refresh();
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
            chart.Axes.Right.Visible = true;
            chart.Axes.Right.AxisPen.Visible = true;
            chart.Axes.Top.Visible = true;
            chart.Axes.Top.AxisPen.Visible = true;
            chart.Axes.Bottom.MinorGrid.Visible = false;
            

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
                        PlotSeries(await SeriesQuery.Invoke(mean));
                        break;

                    case TagType.Plot:
                        query.PlotId = tag.ID;
                        PlotSeries(await SeriesQuery.Invoke(query));
                        break;

                    default:
                        if (node.Text == "All")
                        {
                            foreach (TreeNode plot in node.Parent.Nodes)
                            {
                                if (plot.Tag is NodeTag t && t.Type == TagType.Plot)
                                {
                                    query.PlotId = t.ID;

                                    PlotSeries(await SeriesQuery.Invoke(query));
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

            chart.Text = dates[date].ToString();

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
                        var mean = new MeanTreatmentDataByTraitQuery()
                        {
                            TraitName = trait,
                            TreatmentId = (int)node.Tag
                        };
                        PlotSeries(await SeriesQuery.Invoke(mean));
                        break;

                    case TagType.Plot:
                        query.PlotId = tag.ID;
                        PlotSeries(await SeriesQuery.Invoke(query));
                        break;

                    case TagType.Empty:
                        if (node.Name == "All")
                        {
                            foreach (TreeNode plot in node.Nodes)
                            {
                                if (plot.Tag is NodeTag t && t.Type == TagType.Plot)
                                {
                                    query.PlotId = t.ID;
                                    PlotSeries(await SeriesQuery.Invoke(query));
                                }
                            }

                        }
                        break;

                    default:
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
