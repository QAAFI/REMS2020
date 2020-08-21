using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

using MediatR;

using Rems.Application;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS.Experiments.Queries.Experiments;
using Rems.Application.DB.Commands;
using Rems.Application.DB.Queries;
using Rems.Application.Entities.Commands;
using Rems.Application.Tables.Queries;
using Rems.Infrastructure;
using Rems.Infrastructure.Excel;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;
using WindowsClient.Forms;

namespace WindowsClient
{
    public partial class REMSClient : Form
    {
        private string _importFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "Data");

        private readonly ClientLogic Logic;

        public REMSClient(IServiceProvider provider)
        {
            Logic = new ClientLogic(provider);          

            InitializeComponent();

            experimentsTree.AfterSelect += OnExperimentNodeChanged;
            traitsBox.SelectedIndexChanged += OnTraitsBoxIndexChanged;

            EventManager.ItemNotFound += OnEntityNotFound;
        }        

        #region Form

        private void OnEntityNotFound(object sender, ItemNotFoundArgs args)
        {
            var selector = new ItemSelector(args);
            selector.ShowDialog();
        }

        protected new async void Close()
        {
            await Logic.TryQueryREMS(new CloseDBCommand(), "The database did not close correctly.");

            experimentsTree.NodeMouseClick -= OnExperimentNodeChanged;

            base.Close();
        }

        #endregion

        #region Taskbar

        /// <summary>
        /// On click, prompt the user to create a new blank database
        /// </summary>
        private async void MenuNewClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                save.AddExtension = true;
                save.Filter = "SQLite (*.db)|*.db";
                save.RestoreDirectory = true;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    await Logic.TryQueryREMS(new CreateDBCommand() { FileName = save.FileName });
                    await Logic.TryQueryREMS(new OpenDBCommand() { FileName = save.FileName });

                    LoadListView();
                }
            }
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private async void MenuOpenClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                open.Filter = "SQLite (*.db)|*.db";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    await Logic.TryQueryREMS(new OpenDBCommand() { FileName = open.FileName });
                    LoadListView();

                    LoadExperimentsTab();
                }
            }
        }

        /// <summary>
        /// On click, saves changes made to the database
        /// </summary>
        private async void MenuSaveClicked(object sender, EventArgs e)
        {
            await Logic.TryQueryREMS(new SaveDBCommand());
        }

        private void MenuSaveAsClicked(object sender, EventArgs e)
        {
            // TODO: Implement
            // string file = ?
            // Logic.TryQueryREMS(new SaveAsDbCommand() { FileName = file });
        }

        #region Import

        private void ImportInformationClicked(object sender, EventArgs e)
        {
            // Even though the import operation is the same, we make a distinction between
            // information, experiments and data for the users sake 
            ImportFile();
        }

        private void ImportExperimentsClicked(object sender, EventArgs e)
        {
            // Even though the import operation is the same, we make a distinction between
            // information, experiments and data for the users sake 
            ImportFile();
        }

        private void ImportDataClicked(object sender, EventArgs e)
        {
            // Even though the import operation is the same, we make a distinction between
            // information, experiments and data for the users sake 
            ImportFile();
        }

        private async void ImportFile()
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var data = ExcelImporter.ReadRawData(open.FileName);

                        await Logic.TryQueryREMS(new BulkInsertCommand() { Data = data });
                    }
                    catch (IOException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private async void MenuExportClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

                if (save.ShowDialog() == DialogResult.OK) await Logic.TryDataExport(save.FileName);
            }
        }

        #endregion       

        #region Tabs

        #region Information

        private async void OnRelationsIndexChanged(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;
            dataGridView.DataSource = await Logic.TryQueryREMS(new GetDataTableQuery() { TableName = item });
        }

        private async void LoadListView()
        {
            var items = await Logic.TryQueryREMS(new GetTableListQuery());

            relationsListBox.Items.Clear();
            relationsListBox.Items.AddRange(items.ToArray());
        }

        #endregion

        #region Experiments

        private struct ExperimentTag
        {
            public int ID { get; set; }
        }

        private struct TreatmentTag
        {
            public int ID { get; set; }
        }

        private struct PlotTag
        {
            public int ID { get; set; }
        }

        /// <summary>
        /// Process the currently selected experiments tab
        /// </summary>
        private void LoadExperimentsTab()
        {
            LoadTreeView();
            LoadCropTab();
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        private async void LoadTreeView()
        {
            experimentsTree.Nodes.Clear();

            var exps = await Logic.TryQueryREMS(new ExperimentsQuery());

            foreach (var exp in exps)
            {
                TreeNode eNode = new TreeNode(exp.Value) { Tag = new ExperimentTag() { ID = exp.Key } };

                var treatments = await Logic.TryQueryREMS(new TreatmentsQuery() { ExperimentId = exp.Key });

                foreach (var treatment in treatments)
                {
                    TreeNode tNode = new TreeNode(treatment.Value)
                    {
                        Tag = new TreatmentTag() { ID = treatment.Key }
                    };
                    tNode.Nodes.Add(new TreeNode("All"));

                    var plots = await Logic.TryQueryREMS(new PlotsQuery() { TreatmentId = treatment.Key });

                    tNode.Nodes.AddRange(plots.Select(p => new TreeNode(p.Value) { Tag = new PlotTag() { ID = p.Key } }).ToArray()) ;
                    eNode.Nodes.Add(tNode);
                }                

                experimentsTree.Nodes.Add(eNode);
            }
            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();
        }

        private async void LoadCropTab()
        {
            // Load the trait type box
            traitTypeBox.Items.Clear();
            var types = await Logic.TryQueryREMS(new TraitTypesQuery());

            if (types.Length == 0) return;

            traitTypeBox.Items.AddRange(types);
            traitTypeBox.SelectedIndex = 0;

            // Load the traits box
            RefreshTraitsBox();
        }

        private void OnTraitTypeBoxSelectionChanged(object sender, EventArgs e) => RefreshTraitsBox();

        private void OnTraitsBoxIndexChanged(object sender, EventArgs e) => RefreshChart();

        private void OnExperimentNodeChanged(object sender, EventArgs e)
        {
            RefreshSummary();
            RefreshOperationsData();
            RefreshChart();
        }

        private void OnOperationsBoxSelectionChanged(object sender, EventArgs e)
        {
            RefreshOperationsData();
        }

        private async void RefreshSummary()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            if (node.Tag is ExperimentTag tag)
            {
                var query = new ExperimentSummary() { ExperimentId = tag.ID };

                var experiment = await Logic.TryQueryREMS(query);

                descriptionBox.Text = experiment["Description"];
                designBox.Text = experiment["Design"];
                cropBox.Content = experiment["Crop"];
                fieldBox.Content = experiment["Field"];
                metBox.Content = experiment["Met"];
                repsBox.Content = experiment["Reps"];
                ratingBox.Content = experiment["Rating"];
                startBox.Content = experiment["Start"];
                endBox.Content = experiment["End"];

                var items = experiment["List"].Split('\n');
                researchersBox.Items.Clear();
                researchersBox.Items.AddRange(items);

                notesBox.Text = experiment["Notes"];

                var sowing = await Logic.TryQueryREMS(new SowingSummary() { ExperimentId = tag.ID });
                sowingMethodBox.Content = sowing["Method"];
                sowingDateBox.Content = sowing["Date"];
                sowingDepthBox.Content = sowing["Depth"];
                sowingRowBox.Content = sowing["Row"];
                sowingPopBox.Content = sowing["Pop"];

                var design = new DesignsTableQuery() { ExperimentId = tag.ID };
                designData.DataSource = await Logic.TryQueryREMS(design);
            }
        }

        private async void RefreshOperationsData()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            int id;
            if (node.Tag is TreatmentTag tag) 
                id = tag.ID;
            else if (node.Tag is PlotTag)
                id = ((TreatmentTag)node.Parent.Tag).ID;
            else return;

            IRequest<SeriesData> query;

            string item = operationsBox.SelectedItem?.ToString();
            if (item == "Irrigations")
                query = new IrrigationDataQuery() { TreatmentId = id };
            else if (item == "Fertilizations")
                query = new FertilizationDataQuery() { TreatmentId = id };
            else if (item == "Tillages")
                query = new TillagesDataQuery() { TreatmentId = id };
            else
                return;

            var data = await Logic.TryQueryREMS(query);

            operationsChart.Series.Clear();
            operationsChart.Text = item;

            // Data
            Bar bar = new Bar();
            bar.CustomBarWidth = 8;
            bar.Marks.Visible = false;

            bar.Add(data.X, data.Y);

            // X-Axis
            bar.XValues.DateTime = true;
            //operationsChart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
            //operationsChart.Axes.Bottom.Labels.Angle = 90;
            operationsChart.Axes.Bottom.Title.Text = data.XLabel;
            //operationsChart.Axes.Bottom.Ticks.Visible = true;

            // Y-Axis
            operationsChart.Axes.Left.Title.Text = data.YLabel;

            // Legend            
            bar.Legend.Visible = false;


            operationsChart.Series.Add(bar);
        }

        private async void RefreshTraitsBox()
        {
            traitsBox.Items.Clear();
            var traits = await Logic.TryQueryREMS(new TraitsByTypeQuery() { Type = traitTypeBox.SelectedItem.ToString() });
            traitsBox.Items.AddRange(traits);
            traitsBox.Refresh();
        }

        private void RefreshChart()
        {
            leftBtn.Visible = false;
            rightBtn.Visible = false;

            if (traitTypeBox.Text == "Crop") RefreshCropData();
            if (traitTypeBox.Text == "SoilLayer") RefreshSoilControl();
        }

        private async void RefreshCropData()
        {
            cropChart.Axes.Custom.Clear();
            cropChart.Series.Clear();

            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            Axis y = new Axis(cropChart.Chart)
            {
                Horizontal = false,
                AutomaticMinimum = false,
                AutomaticMaximum = false,
                Minimum = 0,
                Maximum = 0.00000001                
            };

            cropChart.Axes.Custom.Add(y);
            cropChart.Text = "Crop Traits";

            foreach (string trait in traitsBox.CheckedItems)
            {
                var bounds = await Logic.TryQueryREMS(new PlotDataTraitBoundsQuery() { TraitName = trait });

                if (bounds.YMin < y.Minimum) y.Minimum = bounds.YMin - bounds.YMin / 10;
                if (bounds.YMax > y.Maximum) y.Maximum = bounds.YMax + bounds.YMax / 10;

                var query = new PlotDataByTraitQuery() { TraitName = trait };

                if (node.Tag is ExperimentTag)
                {                    
                    // TODO: Decide what to implement at experiment level
                    // Simply disable crop tab?
                }
                else if (node.Tag is TreatmentTag tag)
                {
                    var mean = new MeanTreatmentDataByTraitQuery()
                    {
                        TraitName = trait,
                        TreatmentId = tag.ID
                    };
                    var data = await Logic.TryQueryREMS(mean);
                    PlotSingleData(data, y);                    
                }
                else if (node.Text == "All")
                {
                    foreach (TreeNode plot in node.Parent.Nodes)
                    {
                        if (plot.Tag is PlotTag t)
                        {
                            query.PlotId = t.ID;

                            var data = await Logic.TryQueryREMS(query);
                            PlotSingleData(data, y);
                        }
                    }
                }
                else if (node.Tag is PlotTag plot)
                {
                    query.PlotId = plot.ID;
                    var data = await Logic.TryQueryREMS(query);
                    PlotSingleData(data, y);
                }
                
            }
            
        }

        private async void RefreshSoilData()
        {
            cropChart.Axes.Custom.Clear();
            cropChart.Series.Clear();

            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            Axis y = new Axis(cropChart.Chart)
            {
                Horizontal = false,
                Inverted = true,
                Minimum = 0
            };

            cropChart.Axes.Custom.Add(y);
            cropChart.Text = dates[index].ToString();

            foreach (string trait in traitsBox.CheckedItems)
            {
                var query = new TraitDataOnDateQuery()
                {
                    TraitName = trait,
                    Date = dates[index]
                };

                if (node.Tag is ExperimentTag)
                {
                    // TODO: Decide what to implement at experiment level
                }
                else if (node.Tag is TreatmentTag)
                {
                    foreach (TreeNode plot in node.Nodes)
                    {
                        if (plot.Tag is PlotTag tag)
                        {
                            query.PlotId = tag.ID;

                            var data = await Logic.TryQueryREMS(query);
                            PlotSingleData(data, y);
                        }
                    }
                }
                else if (node.Tag is PlotTag plot)
                {
                    query.PlotId = plot.ID;
                    var data = await Logic.TryQueryREMS(query);
                    PlotSingleData(data, y);
                }
                else if (node.Text == "Mean")
                {
                    //var mean = new MeanTreatmentDataByTraitQuery()
                    //{
                    //    TraitName = trait,
                    //    TreatmentId = (int)node.Tag
                    //};

                    //PlotSingleData(mean, y);
                }
            }
        }

        /// <summary>
        /// Plot a single set of data
        /// </summary>
        private void PlotSingleData(SeriesData data, Axis YAxis)
        {
            if (data is null) return;
            if (data.X.Length == 0) return;
            
            YAxis.Title.Text = data.YLabel;

            Points points = new Points();
            points.Legend.Text = data.Name;                        
            points.CustomVertAxis = YAxis;

            Line line = new Line();
            line.Legend.Visible = false;
            line.CustomVertAxis = YAxis;

            if (data.X.GetValue(0) is DateTime)
            {
                points.XValues.DateTime = true;
                line.XValues.DateTime = true;

                cropChart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
            }

            line.Add(data.X, data.Y);
            points.Add(data.X, data.Y);
            
            cropChart.Series.Add(line);
            cropChart.Series.Add(points);

            line.Color = points.Color;
        }

        
        private async void RefreshSoilControl()
        {
            if (!(await RefreshSoilDataDates())) return;

            leftBtn.Visible = true;
            rightBtn.Visible = true;
            
            RefreshSoilData();            
        }
        

        private DateTime[] dates;
        private int index = 0;
        private async Task<bool> RefreshSoilDataDates()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return false;

            if (node.Tag is ExperimentTag)
            {
                return false;
            }
            else if (node.Tag is TreatmentTag tag)
            {
                dates = await Logic.TryQueryREMS(new SoilLayerDatesQuery() { TreatmentId = tag.ID });
            }
            else
            {
                int id = ((TreatmentTag)node.Parent.Tag).ID;
                dates = await Logic.TryQueryREMS(new SoilLayerDatesQuery() { TreatmentId = id });
            }
            index = 0;

            if (dates.Length > 0) return true;
            
            return false;
        }

        private void OnLeftBtnClicked(object sender, EventArgs e)
        {
            index--;
            if (index < 0) index = dates.Length - 1;

            RefreshSoilData();
        }

        private void OnRightBtnClicked(object sender, EventArgs e)
        {
            index++;
            if (index > dates.Length - 1) index = 0;

            RefreshSoilData();
        }



        #endregion

        #endregion
    }
}
