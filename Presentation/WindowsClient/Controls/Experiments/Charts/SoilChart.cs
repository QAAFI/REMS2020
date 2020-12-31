using System;
using System.Windows.Forms;

using Steema.TeeChart.Styles;

using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System.Threading.Tasks;
using Steema.TeeChart;
using System.Collections.Generic;
using System.Linq;
using WindowsClient.Models;
using MediatR;

namespace WindowsClient.Controls
{
    public partial class SoilChart : UserControl
    {
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public Func<int, TreeNode, Task> Updater;

        private int treatment = -1;
        private int plot;
        private TreeNode selected;
        private Chart chart => tChart.Chart;
        private IEnumerable<string> traits => traitsBox.SelectedItems.Cast<string>();
        //private DateTime date => Convert.ToDateTime(datesBox.SelectedItem);
        private IEnumerable<DateTime> dates => datesBox.SelectedItems.Cast<DateTime>();

        public SoilChart()
        {
            InitializeComponent();

            // Set the titles
            tChart.Text = "Soil Traits";
            chart.Axes.Left.Title.Text = "Depth";
            chart.Axes.Bottom.Title.Text = "Value";

            chart.Panel.MarginUnits = Steema.TeeChart.PanelMarginUnits.Pixels;
            chart.Panel.MarginBottom = 20;

            chart.Axes.Left.AutomaticMinimum = false;
            chart.Axes.Left.Minimum = 0;
            chart.Axes.Left.Inverted = true;

            chart.Axes.Bottom.Minimum = 0;
            chart.Axes.Bottom.AutomaticMinimum = false;
            chart.Axes.Bottom.Maximum = 1;
            chart.Axes.Bottom.AutomaticMaximum = false;

            traitsBox.SelectedIndexChanged += OnTraitSelected;
        }

        private async void OnTraitSelected(object sender, EventArgs e) => await ChangeData(selected);

        private async void OnDateSelected(object sender, EventArgs e) => await ChangeData(selected);

        public async Task ChangeData(TreeNode node)
        {
            int id = treatment;

            if (Updater == UpdateSingle) id = plot;

            if (InvokeRequired)
                Invoke(new Func<TreeNode, Task>(ChangeData), node);
            else
                await Task.Run(() => { if (Updater != null) Updater(id, node); });
        }

        public async Task LoadBoxes(int id)
        {
            if (id == treatment)
                return;
            else
                treatment = id;

            await LoadTraitsBox(id);
            await LoadDatesBox(id);
        }

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

        public async Task UpdateSingle(int id, TreeNode node)
        {
            if (InvokeRequired)
                Invoke(new Func<int, TreeNode, Task>(UpdateSingle), id, node);
            else
            {
                if (node.Name != "All")
                    chart.Series.Clear();

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

        public async Task UpdateMean(int id, TreeNode node)
        {
            if (InvokeRequired)
                Invoke(new Func<int, TreeNode, Task>(UpdateMean), id, node);
            else
            {
                chart.Series.Clear();

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

        public async Task UpdateAll(int id, TreeNode node)
        {
            if (InvokeRequired)
                Invoke(new Func<int, TreeNode, Task>(UpdateAll), id, node);
            else
            {
                chart.Series.Clear();

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
