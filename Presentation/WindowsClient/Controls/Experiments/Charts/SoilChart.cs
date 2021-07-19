using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.CQRS;
using Steema.TeeChart;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of soil data for a treatment
    /// </summary>
    public partial class SoilChart : UserControl, ITreatmentControl
    {
        /// <inheritdoc/>
        public int Treatment { get; set; }

        private Chart chart => tChart.Chart;

        private Dictionary<string, string> descriptions = new Dictionary<string, string>();
        private string[] traits => traitsBox.SelectedItems.Cast<string>().ToArray();
        private DateTime[] dates => datesBox.SelectedItems.Cast<DateTime>().ToArray();

        public SoilChart()
        {
            InitializeComponent();

            var tip = new ToolTip();

            plotsBox.SelectedIndex = 0;
            plotsBox.SelectedIndexChanged += async (s, e) => await LoadPlots();

            traitsBox.MouseHover += (s, e) => OnTraitMouseHover(tip);
            traitsBox.SelectedIndexChanged += OnTraitSelected;
        }

        /// <summary>
        /// Sets the default style of the chart
        /// </summary>
        public async Task Initialise(int experiment)
        {
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
            chart.Axes.Bottom.Minimum = 0;
            chart.Axes.Bottom.AutomaticMinimum = false;
            chart.Axes.Bottom.Maximum = 1;
            chart.Axes.Bottom.AutomaticMaximum = false;
        }

        private async void OnTraitSelected(object sender, EventArgs e) 
            => await LoadPlots();

        private async void OnDateSelected(object sender, EventArgs e) 
            => await LoadPlots();

        /// <summary>
        /// Sets the tool tip on mouse hover
        /// </summary>
        private void OnTraitMouseHover(ToolTip tip)
        {
            var mouse = MousePosition;
            var client = traitsBox.PointToClient(mouse);
            int index = traitsBox.IndexFromPoint(client);

            if (index == -1) return;

            var trait = traitsBox.Items[index].ToString();
            var text = descriptions[trait];
            tip.SetToolTip(traitsBox, text);
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
                plotsBox.Items.Add(new PlotDTO { ID = plot.Key, Name = plot.Value });
        }

        /// <summary>
        /// Fills the traits box with all traits in a treatment that have graphable data
        /// </summary>
        /// <param name="id">The treatment ID</param>
        public async Task LoadTraitsBox()
        {
            // Load the trait type box
            var traits = await QueryManager.Request(new SoilTraitsQuery() { TreatmentId = Treatment });
            descriptions = await QueryManager.Request(new TraitDescriptionsQuery { Traits = traits });

            lock (traitsBox)
            {
                traitsBox.Items.Clear();

                if (traits.Length < 1) return;

                traitsBox.Items.AddRange(traits);
                traitsBox.SelectedIndex = 0;
            }
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

        public async Task LoadPlots()
        {
            chart.Series.Clear();

            if (plotsBox.SelectedItem.ToString() == "All")
                await UpdateAll();

            else if (plotsBox.SelectedItem.ToString() == "Mean")
                await UpdateMean();

            else if (plotsBox.SelectedItem is PlotDTO plot)
                await UpdateSingle(plot.ID);
        }

        /// <summary>
        /// Updates the displayed data for a single plot
        /// </summary>
        /// <param name="id">The plot ID</param>
        /// <param name="node">The selected node</param>
        public async Task UpdateSingle(int id)
        {
            tChart.Text = "Soil trait values for a single treatment plot";

            foreach (DateTime date in dates)
            {
                foreach (string trait in traits)
                {
                    var query = new SoilLayerTraitDataQuery
                    {
                        TraitName = trait,
                        PlotId = id,
                        Date = date
                    };

                    var data = await QueryManager.Request(query);
                    data.AddToChart(chart, true);
                }
            }
        }

        /// <summary>
        /// Updates the displayed data for the average of all plots in a treatment
        /// </summary>
        /// <param name="id">The treatment ID</param>
        /// <param name="node">The selected node</param>
        public async Task UpdateMean()
        {
            tChart.Text = "Average soil trait values across all treatment plots";

            foreach (DateTime date in dates)
            {
                foreach (string trait in traits)
                {
                    var query = new MeanSoilTraitDataQuery
                    {
                        TraitName = trait,
                        TreatmentId = Treatment,
                        Date = date
                    };

                    var data = await QueryManager.Request(query);
                    data.AddToChart(chart, true);
                }
            }
        }

        /// <summary>
        /// Updates the displayed data for all the plots in a treatment
        /// </summary>
        /// <param name="id">The treatment ID</param>
        /// <param name="node">The selected node</param>
        public async Task UpdateAll()
        {
            tChart.Text = "Comparison of soil trait values across all treatment plots";

            foreach (DateTime date in dates)
            {
                foreach (string trait in traits)
                {
                    var query = new AllSoilTraitDataQuery
                    {
                        TraitName = trait,
                        TreatmentId = Treatment,
                        Date = date
                    };

                    var series = await QueryManager.Request(query);

                    foreach (var data in series)
                        data.AddToChart(chart, true);
                }
            }
        }        
    }
}
