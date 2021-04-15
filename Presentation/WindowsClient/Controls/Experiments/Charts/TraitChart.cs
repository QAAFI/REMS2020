﻿using System;
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
    /// Manages the presentation of trait data for a treatment
    /// </summary>
    public partial class TraitChart : UserControl
    {
        /// <summary>
        /// Tracks which update function to call when a trait is selected
        /// </summary>
        public Func<int, TreeNode, Task> Updater;

        private int treatment = -1;
        private int plot;
        private TreeNode selected;

        private Chart chart => tChart.Chart;

        private Dictionary<string, string> descriptions = new Dictionary<string, string>();
        private IEnumerable<string> traits => traitsBox.SelectedItems.Cast<string>().ToArray();

        public TraitChart()
        {
            InitializeComponent();
            InitialiseChart();

            var tip = new ToolTip();

            traitsBox.MouseHover += (s, e) => OnTraitMouseHover(tip);
            traitsBox.SelectedIndexChanged += OnTraitSelected;            
        }

        /// <summary>
        /// Sets the default style of the chart
        /// </summary>
        private void InitialiseChart()
        {
            // Set the titles
            tChart.Text = "Crop Traits";
            chart.Axes.Left.Title.Text = "Value";
            chart.Axes.Bottom.Title.Text = "Date";

            // Set the margins
            chart.Panel.MarginUnits = Steema.TeeChart.PanelMarginUnits.Pixels;
            chart.Panel.MarginRight = 20;
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

        private async void OnTraitSelected(object sender, EventArgs e) => await DisplayNodeData(selected);

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
        /// Fills the traits box with all traits in a treatment that have graphable data
        /// </summary>
        /// <param name="id">The treatment ID</param>
        public async Task LoadTraitsBox(int id)
        {
            if (InvokeRequired)
                Invoke(new Func<int, Task>(LoadTraitsBox), id);
            else
            {
                if (id == treatment)
                    return;
                else
                    treatment = id;                

                // Load the trait type box
                var traits = await QueryManager.Request(new CropTraitsQuery() { TreatmentId = id });
                descriptions = await QueryManager.Request(new TraitDescriptionsQuery { Traits = traits });

                lock (traitsBox)
                {
                    traitsBox.Items.Clear();

                    if (traits.Length < 1) return;

                    traitsBox.Items.AddRange(traits);
                    traitsBox.SelectedIndex = 0;
                }
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

                tChart.Text = "Trait values for a treatment plot";

                plot = id;
                selected = node;

                foreach (string trait in traits)
                {
                    var query = new PlotDataByTraitQuery
                    {
                        TraitName = trait,
                        PlotId = id
                    };

                    var data = await QueryManager.Request(query);
                    data.AddToChart(chart);
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

                tChart.Text = "Average trait values across all treatment plots";

                treatment = id;
                selected = node;

                foreach (string trait in traits)
                {
                    var query = new MeanCropTraitDataQuery
                    {
                        TraitName = trait,
                        TreatmentId = id
                    };

                    var data = await QueryManager.Request(query);
                    data.AddToChart(chart);
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

                tChart.Text = "Comparison of trait values across all treatment plots";

                if (node is null) return;

                treatment = id;
                selected = node;

                foreach (string trait in traits)
                {
                    var query = new AllCropTraitDataQuery
                    {
                        TraitName = trait,
                        TreatmentId = id
                    };

                    var series = await QueryManager.Request(query);

                    foreach (var data in series)
                        data.AddToChart(chart);
                }

                Updater = UpdateAll;
            }
        }
    
    }
}
