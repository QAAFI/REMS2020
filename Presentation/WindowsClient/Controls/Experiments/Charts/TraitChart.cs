using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.CQRS;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of trait data for a treatment
    /// </summary>
    public partial class TraitChart : UserControl, ITreatmentControl
    {
        /// <inheritdoc/>
        public int Treatment { get; set; }

        public string TraitType { get; set; }

        protected Chart chart 
            => tChart.Chart;

        protected object selected 
            => plotsBox.SelectedItem;

        protected string[] traits 
            => traitsBox.SelectedItems.OfType<ListTrait>().Select(p => p.Name).ToArray();

        protected string[] descriptions 
            => traitsBox.SelectedItems.OfType<ListTrait>().Select(p => p.Description).ToArray();

        public TraitChart()
        {
            InitializeComponent();
            Format();            

            var tip = new ToolTip();

            chartBox.SelectedIndex = 0;
            chartBox.SelectedIndexChanged += async (s, e) => await LoadPlots().TryRun();

            plotsBox.SelectedIndex = 0;
            plotsBox.SelectedIndexChanged += async (s, e) => await LoadPlots().TryRun();

            traitsBox.MouseHover += (s, e) => OnTraitMouseHover(tip);
            traitsBox.SelectedIndexChanged += async (s, e) => await LoadPlots().TryRun();
        }

        private void Format()
        {
            // Set the legend
            chart.Legend.HorizMargin = -2;
            chart.Legend.Title.Visible = true;

            // Set the titles
            tChart.Text = "Crop Traits";
            chart.Axes.Left.Title.Text = "Value";
            chart.Axes.Bottom.Title.Text = "Date";

            // Set the margins
            chart.Panel.MarginUnits = Steema.TeeChart.PanelMarginUnits.Pixels;
            chart.Panel.MarginLeft = 10;
            chart.Panel.MarginRight = 0;
            chart.Panel.MarginBottom = 30;

            // Force the Y-Axis to stop at 0
            chart.Axes.Left.AutomaticMinimum = false;
            chart.Axes.Left.Minimum = 0;

            // Configure the X-Axis for date data
            chart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
            chart.Axes.Bottom.Labels.Angle = 60;
            chart.Axes.Bottom.Ticks.Visible = true;
            chart.Axes.Bottom.Title.AutoPosition = true;
        }

        /// <summary>
        /// Sets the default style of the chart
        /// </summary>
        public async Task Initialise(int experiment)
        {
            var begin = await QueryManager.Request(new BeginQuery { ID = experiment });
            var end = await QueryManager.Request(new EndQuery { ID = experiment });
            chart.Axes.Bottom.Minimum = begin.ToOADate();
            chart.Axes.Bottom.Maximum = end.ToOADate();
        }

        /// <summary>
        /// Sets the tool tip on mouse hover
        /// </summary>
        private void OnTraitMouseHover(ToolTip tip)
        {
            var mouse = MousePosition;
            var client = traitsBox.PointToClient(mouse);
            int index = traitsBox.IndexFromPoint(client);

            if (index == -1) return;

            if (traitsBox.Items[index] is ListTrait pair)
                tip.SetToolTip(traitsBox, pair.Description);
        }

        public async Task LoadTreatment(int id)
        {
            Treatment = id;

            await AddPlots();
            await LoadTraitsBox();
        }  

        public async Task AddPlots()
        {
            var plots = await QueryManager.Request(new PlotsQuery { TreatmentId = Treatment });

            foreach (var plot in plots)
                plotsBox.Items.Add(new PlotDTO { ID = plot.Key, Name = plot.Value });
        }

        /// <summary>
        /// Fills the traits box with all traits in a treatment that have graphable data
        /// </summary>
        /// <param name="id">The treatment ID</param>
        public async Task LoadTraitsBox()
        {
            var query = new PlotTraitsByTypeQuery
            {
                TreatmentId = Treatment,
                TraitType = TraitType
            };

            // Load the trait type box
            var names = await QueryManager.Request(query);
            var pairs = await QueryManager.Request(new TraitDescriptionsQuery { Traits = names });

            lock (traitsBox)
            {
                traitsBox.Items.Clear();

                if (names.Length < 1) return;

                foreach (var pair in pairs)
                    traitsBox.Items.Add(new ListTrait { Name = pair.Key, Description = pair.Value });
            }

            traitsBox.SelectedIndex = 0;
        }        

        public virtual async Task LoadPlots()
        {
            string xtitle = "";
            string ytitle = "";

            List<SeriesData<DateTime, double>> datas = new();
            Action<SeriesData<DateTime, double>> iterator = data =>
            {
                datas.Add(data);
                xtitle = data.XName;
                ytitle = data.YName;
            };

            if (selected.ToString() == "All")
                foreach (var plot in await QueryManager.Request(new PlotsQuery { TreatmentId = Treatment }))
                    await new PlotDataByTraitQuery
                    {
                        TraitName = "",
                        PlotId = plot.Key
                    }.IterateTraits(traits, iterator);

            else if (selected.ToString() == "Mean")
                await new MeanCropTraitDataQuery
                {
                    TraitName = "",
                    TreatmentId = Treatment
                }.IterateTraits(traits, iterator);

            else if (selected is PlotDTO plot)
                await new PlotDataByTraitQuery
                {
                    TraitName = "",
                    PlotId = plot.ID
                }.IterateTraits(traits, iterator);

            chart.Series.Clear();
            Action<SeriesData<DateTime, double>> action = chartBox.SelectedItem switch
            {
                "Scatter" => d =>
                {
                    var series = d.CreateSeries<Points, DateTime, double>();
                    series.Pointer.Style = (PointerStyles)(d.Series % 16);
                    chart.Series.Add(series);
                },

                "Bar" => d =>
                {
                    var series = d.CreateSeries<Bar, DateTime, double>();
                    series.Marks.Visible = false;
                    series.CustomBarWidth = 3;
                    chart.Series.Add(series);
                },

                "Line" => d =>
                {
                    var series = d.CreateSeries<Line, DateTime, double>();
                    series.Pointer.Style = PointerStyles.Nothing;
                    chart.Series.Add(series);
                },

                _ => d =>
                {
                    var points = d.CreateSeries<Points, DateTime, double>();
                    points.Pointer.Style = (PointerStyles)(d.Series % 16);
                    var line = d.CreateSeries<Line, DateTime, double>();
                    line.Legend.Visible = false;
                    chart.Series.Add(points);
                    chart.Series.Add(line);
                }
            };
            datas.ForEach(action);
            chart.Axes.Bottom.Title.Text = xtitle;
            chart.Axes.Left.Title.Text = ytitle;
            chart.Legend.Title.Text = descriptions[0]?.WordWrap(18) ?? "";

            chart.Legend.Width = 120;

            chart.Header.Text = await QueryManager.Request(new TreatmentDesignQuery { TreatmentId = Treatment });

        }
    }
}
