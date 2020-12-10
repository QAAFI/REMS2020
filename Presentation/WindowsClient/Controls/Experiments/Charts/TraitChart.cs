using System;
using System.Linq;
using System.Windows.Forms;

using Steema.TeeChart.Styles;

using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System.Threading.Tasks;
using WindowsClient.Models;
using Steema.TeeChart;
using Models;
using System.Collections.Generic;

namespace WindowsClient.Controls
{
    public partial class TraitChart : UserControl
    {
        public event QueryHandler DataRequested;

        public Func<int, TreeNode, Task> Updater;

        private int treatment = -1;
        private int plot;
        private TreeNode selected;

        private Chart chart => tChart.Chart;

        //private string trait => traitsBox.SelectedItem?.ToString();

        private IEnumerable<string> traits => traitsBox.SelectedItems.Cast<string>();

        public TraitChart()
        {
            InitializeComponent();

            // Set the titles
            tChart.Text = "Crop Traits";
            chart.Axes.Left.Title.Text = "Value";
            chart.Axes.Bottom.Title.Text = "Date";

            // Set the margins
            chart.Panel.MarginUnits = Steema.TeeChart.PanelMarginUnits.Pixels;
            chart.Panel.MarginRight = 20;
            chart.Panel.MarginBottom = 50;

            // Force the Y-Axis to stop at 0
            chart.Axes.Left.AutomaticMinimum = false;
            chart.Axes.Left.Minimum = 0;

            // Configure the X-Axis for date data
            chart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
            chart.Axes.Bottom.Labels.Angle = 60;
            chart.Axes.Bottom.Ticks.Visible = true;
            chart.Axes.Bottom.Title.AutoPosition = true;

            traitsBox.SelectedIndexChanged += OnTraitSelected;
        }

        private async void OnTraitSelected(object sender, EventArgs e) => await ChangeData(plot, selected);

        public async Task RefreshData() => await ChangeData(plot, selected);

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

                traitsBox.Items.Clear();

                // Load the trait type box
                var types = await DataRequested.Send(new TreatmentTraitsQuery() { TreatmentId = id });

                if (types.Length == 0) return;

                traitsBox.Items.AddRange(types);
            }
        }

        public async Task ChangeData(int id, TreeNode node)
        {
            if (InvokeRequired)
                Invoke(new Func<int, TreeNode, Task>(ChangeData), id, node);
            else
                await Task.Run(() => { if (Updater != null) Updater(id, node); });        
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

                foreach (string trait in traits)
                {
                    var query = new PlotDataByTraitQuery
                    {
                        TraitName = trait,
                        PlotId = id
                    };

                    var data = await DataRequested.Send(query);
                    data.AddToChart(chart);
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

                plot = id;
                selected = node;

                foreach (string trait in traits)
                {
                    var query = new MeanTreatmentDataByTraitQuery
                    {
                        TraitName = trait,
                        TreatmentId = id
                    };

                    var data = await DataRequested.Send(query);
                    data.AddToChart(chart);
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

                selected = node;

                foreach (string trait in traits)
                {
                    var query = new AllDataByTraitQuery
                    {
                        TraitName = trait,
                        TreatmentId = id
                    };

                    var series = await DataRequested.Send(query);

                    foreach (var data in series)
                        data.AddToChart(chart);
                }

                Updater = UpdateAll;
            }
        }
    
    }
}
