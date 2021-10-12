using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.CQRS;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of soil data for a treatment
    /// </summary>
    public partial class SoilLayerChart : UserControl, ITreatmentControl
    {
        /// <inheritdoc/>
        public int Treatment { get; set; }

        protected ColourLookup Colours = new();

        private Chart chart => tChart.Chart;

        private string[] traits => traitsBox.SelectedItems.OfType<ListTrait>().Select(p => p.Name).ToArray();
        private DateTime[] dates => datesBox.SelectedItems.Cast<DateTime>().ToArray();

        public SoilLayerChart()
        {
            InitializeComponent();
            Format();

            var tip = new ToolTip();

            plotsBox.SetItemChecked(0, true);
            plotsBox.SelectedIndexChanged += async (s, e) => await LoadPlots().TryRun();

            traitsBox.MouseHover += (s, e) => OnTraitMouseHover(tip);
            traitsBox.SelectedIndexChanged += OnTraitSelected;
        }

        private void Format()
        {
            // Set the legend
            chart.Legend.HorizMargin = -2;
            chart.Legend.Title.Visible = true;

            // Set the titles
            tChart.Text = "Soil Traits";
            chart.Axes.Left.Title.Text = "Depth";
            chart.Axes.Bottom.Title.Text = "Value";

            // Set the margins
            chart.Panel.MarginUnits = Steema.TeeChart.PanelMarginUnits.Pixels;
            chart.Panel.MarginBottom = 20;

            // Y-Axis options
            chart.Axes.Left.AutomaticMinimum = false;
            chart.Axes.Left.Minimum = 0;
            chart.Axes.Left.Inverted = true;

            // X-Axis options
            chart.Axes.Bottom.AutomaticMinimum = false;
            chart.Axes.Bottom.AutomaticMaximum = false;
        }

        /// <summary>
        /// Sets the default style of the chart
        /// </summary>
        public async Task Initialise(int experiment)
        {
            // No async initialisatiion needed
            await Task.Delay(0);
        }

        private async void OnTraitSelected(object sender, EventArgs e) 
            => await LoadPlots().TryRun();

        private async void OnDateSelected(object sender, EventArgs e) 
            => await LoadPlots().TryRun();

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
            await LoadDatesBox();
        }

        public async Task AddPlots()
        {
            var plots = await QueryManager.Request(new PlotsQuery { TreatmentId = Treatment });

            foreach (var plot in plots)
                plotsBox.Items.Add(plot.Value);
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
                TraitType = "SoilLayer"
            };
            // Load the trait type box
            var traits = await QueryManager.Request(query);
            var pairs = await QueryManager.Request(new TraitDescriptionsQuery { Traits = traits });

            traitsBox.Items.Clear();

            if (traits.Length < 1) return;

            foreach (var pair in pairs)
                traitsBox.Items.Add(new ListTrait { Name = pair.Key, Description = pair.Value });

            traitsBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Fills the dates box with all dates in a treatment that have graphable data
        /// </summary>
        /// <param name="id">The treatment ID</param>
        private async Task LoadDatesBox()
        {
            var query = new SoilLayerDatesQuery() { TreatmentId = Treatment };
            var items = await QueryManager.Request(query);

            lock (datesBox)
            {
                datesBox.Items.Clear();

                if (items.Length < 1) return;

                foreach (var date in items)
                    datesBox.Items.Add(date);

                datesBox.SelectedIndex = 0;
            }
        }

        private DataSet charts = new("Charts");

        public async Task LoadPlots()
        {
            chart.Series.Clear();

            foreach (var date in dates)            
                foreach (var trait in traits)
                {   
                    if (!charts.Tables.Contains($"{Treatment}_{trait}_{date}"))
                    {
                        var query = new SoilLayerDataQuery { TreatmentId = Treatment, TraitName = trait, Date = date };                        
                        charts.Tables.Add(await QueryManager.Request(query));
                    }

                    var table = charts.Tables[$"{Treatment}_{trait}_{date}"];

                    var rows = table.Rows.Cast<DataRow>();

                    if (!rows.Any())
                        continue;

                    var depths = rows.Select(r => Convert.ToDouble(r["Depth"])).ToArray();

                    foreach (var selected in plotsBox.CheckedItems)
                    {
                        var item = selected.ToString();
                        var values = rows.Select(r => (double)r[item]).ToArray();

                        var p = new Points(chart);
                        var l = new Line(chart);

                        p.Pointer.Style = (PointerStyles)(int.TryParse(item, out int i) ? i % 16 : 0 );
                        p.Color = l.Color = Colours.Lookup(trait).colour;

                        p.XValues.Order = ValueListOrder.None;
                        p.YValues.Order = ValueListOrder.Ascending;

                        p.Add(values, depths);
                        l.Add(values, depths);

                        p.Legend.Text = trait + " " + item;
                        l.Legend.Visible = false;

                        chart.Series.Add(p);
                        chart.Series.Add(l);
                    }
                }

            if (!chart.Series.Any())
                return;

            // Set x-axis bounds            
            var min = chart.Series.Select(s => s.XValues.Minimum)?.Min() ?? 0.1;
            var max = chart.Series.Select(s => s.XValues.Maximum)?.Max() ?? 0.9;
            chart.Axes.Bottom.Minimum = min - ((max - min) * 0.1);
            chart.Axes.Bottom.Maximum = max + ((max - min) * 0.1);
            chart.Axes.Bottom.Title.Text = "Value";
            chart.Axes.Left.Title.Text = "Depth";

            chart.Legend.Title.Text = traitsBox.SelectedItems
                .OfType<ListTrait>()
                .First()?.Description.WordWrap(18);
            chart.Legend.Width = 120;

            chart.Header.Text = await QueryManager.Request(new TreatmentDesignQuery { TreatmentId = Treatment });
        }
        
    }
}
