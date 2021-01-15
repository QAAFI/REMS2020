using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using MediatR;
using Rems.Application.CQRS;
using Steema.TeeChart;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of soil data for a treatment
    /// </summary>
    public partial class SoilChart : UserControl
    {
        /// <summary>
        /// Tracks which update function to call when a trait is selected
        /// </summary>
        public Func<int, TreeNode, Task> Updater;

        /// <summary>
        /// Occurs when data is requested from the mediator
        /// </summary>
        public event Func<object, Task<object>> Query;

        /// <summary>
        /// Safely handles a query
        /// </summary>
        /// <typeparam name="T">The type of data requested</typeparam>
        /// <param name="query">The request object</param>
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        private int treatment = -1;
        private int plot;
        private TreeNode selected;
        private Chart chart => tChart.Chart;

        private IEnumerable<string> traits => traitsBox.SelectedItems.Cast<string>();
        private IEnumerable<DateTime> dates => datesBox.SelectedItems.Cast<DateTime>();

        public SoilChart()
        {
            InitializeComponent();

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

            traitsBox.SelectedIndexChanged += OnTraitSelected;
        }

        private async void OnTraitSelected(object sender, EventArgs e) => await DisplayNodeData(selected);

        private async void OnDateSelected(object sender, EventArgs e) => await DisplayNodeData(selected);

        /// <summary>
        /// Displays the data for the given node
        /// </summary>
        /// <param name="node">The node</param>
        public async Task DisplayNodeData(TreeNode node)
        {
            int id = treatment;

            if (Updater == UpdateSingle) id = plot;

            if (InvokeRequired)
                Invoke(new Func<TreeNode, Task>(DisplayNodeData), node);
            else
                await Task.Run(() => { if (Updater != null) Updater(id, node); });
        }

        /// <summary>
        /// Load the traits / dates box items for the treatment
        /// </summary>
        /// <param name="id">The treatment ID</param>
        public async Task LoadBoxes(int id)
        {
            if (id == treatment)
                return;
            else
                treatment = id;

            await LoadTraitsBox(id);
            await LoadDatesBox(id);
        }

        /// <summary>
        /// Fills the traits box with all traits in a treatment that have graphable data
        /// </summary>
        /// <param name="id">The treatment ID</param>
        public async Task LoadTraitsBox(int id)
        {
            if (InvokeRequired)
                Invoke(new Func<int, Task>(LoadTraitsBox), id);
            else
            {
                traitsBox.Items.Clear();

                // Load the trait type box
                var items = await InvokeQuery(new SoilTraitsQuery() { TreatmentId = id });

                if (items.Length < 1) return;

                traitsBox.Items.AddRange(items);
                traitsBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Fills the dates box with all dates in a treatment that have graphable data
        /// </summary>
        /// <param name="id">The treatment ID</param>
        private async Task LoadDatesBox(int id)
        {
            if (InvokeRequired)
                Invoke(new Func<int, Task>(LoadDatesBox), id);
            else
            {
                datesBox.Items.Clear();

                var query = new SoilLayerDatesQuery() { TreatmentId = id };
                var items = await InvokeQuery(query);

                if (items.Length < 1) return;

                foreach (var date in items)
                    datesBox.Items.Add(date);

                datesBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Updates the displayed data for a single plot
        /// </summary>
        /// <param name="id">The plot ID</param>
        /// <param name="node">The selected node</param>
        public async Task UpdateSingle(int id, TreeNode node)
        {
            if (InvokeRequired)
                Invoke(new Func<int, TreeNode, Task>(UpdateSingle), id, node);
            else
            {
                if (node.Name != "All")
                    chart.Series.Clear();

                tChart.Text = "Soil trait values for a single treatment plot";

                plot = id;
                selected = node;

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

                        var data = await InvokeQuery(query);
                        data.AddToChart(chart);
                    }
                }

                Updater = UpdateSingle;
            }
        }

        /// <summary>
        /// Updates the displayed data for the average of all plots in a treatment
        /// </summary>
        /// <param name="id">The treatment ID</param>
        /// <param name="node">The selected node</param>
        public async Task UpdateMean(int id, TreeNode node)
        {
            if (InvokeRequired)
                Invoke(new Func<int, TreeNode, Task>(UpdateMean), id, node);
            else
            {
                chart.Series.Clear();

                tChart.Text = "Average soil trait values across all treatment plots";

                treatment = id;
                selected = node;
                foreach (DateTime date in dates)
                {
                    foreach (string trait in traits)
                    {
                        var query = new MeanSoilTraitDataQuery
                        {
                            TraitName = trait,
                            TreatmentId = id,
                            Date = date
                        };

                        var data = await InvokeQuery(query);
                        data.AddToChart(chart);
                    }
                }

                Updater = UpdateMean;
            }
        }

        /// <summary>
        /// Updates the displayed data for all the plots in a treatment
        /// </summary>
        /// <param name="id">The treatment ID</param>
        /// <param name="node">The selected node</param>
        public async Task UpdateAll(int id, TreeNode node)
        {
            if (InvokeRequired)
                Invoke(new Func<int, TreeNode, Task>(UpdateAll), id, node);
            else
            {
                chart.Series.Clear();

                tChart.Text = "Comparison of soil trait values across all treatment plots";

                if (node is null) return;

                treatment = id;
                selected = node;

                foreach (DateTime date in dates)
                {
                    foreach (string trait in traits)
                    {
                        var query = new AllSoilTraitDataQuery
                        {
                            TraitName = trait,
                            TreatmentId = id,
                            Date = date
                        };

                        var series = await InvokeQuery(query);

                        foreach (var data in series)
                            data.AddToChart(chart);
                    }
                }

                Updater = UpdateAll;
            }
        }
    }
}
