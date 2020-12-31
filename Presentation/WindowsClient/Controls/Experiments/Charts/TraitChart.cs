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
    public partial class TraitChart : UserControl
    {
        public Func<int, TreeNode, Task> Updater;

        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);        

        private int treatment = -1;
        private int plot;
        private TreeNode selected;

        private Chart chart => tChart.Chart;

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

        private async void OnTraitSelected(object sender, EventArgs e) => await ChangeData(selected);

        public async Task ChangeData(TreeNode node)
        {
            int id = treatment;

            if (Updater == UpdateSingle) id = plot;

            if (InvokeRequired)
                Invoke(new Func<TreeNode, Task>(ChangeData), node);
            else
                await Task.Run(() => { if (Updater != null) Updater(id, node); });
        }        

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
                var types = await InvokeQuery(new CropTraitsQuery() { TreatmentId = id });

                if (types.Length < 1) return;

                traitsBox.Items.AddRange(types);
                traitsBox.SelectedIndex = 0;
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

                foreach (string trait in traits)
                {
                    var query = new PlotDataByTraitQuery
                    {
                        TraitName = trait,
                        PlotId = id
                    };

                    var data = await InvokeQuery(query);
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

                treatment = id;
                selected = node;

                foreach (string trait in traits)
                {
                    var query = new MeanCropTraitDataQuery
                    {
                        TraitName = trait,
                        TreatmentId = id
                    };

                    var data = await InvokeQuery(query);
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

                treatment = id;
                selected = node;

                foreach (string trait in traits)
                {
                    var query = new AllCropTraitDataQuery
                    {
                        TraitName = trait,
                        TreatmentId = id
                    };

                    var series = await InvokeQuery(query);

                    foreach (var data in series)
                        data.AddToChart(chart);
                }

                Updater = UpdateAll;
            }
        }
    
    }
}
