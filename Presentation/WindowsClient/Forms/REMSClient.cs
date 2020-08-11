using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

            WindowState = FormWindowState.Maximized;

            InitializeComponent();
            InitializeControls();

            experimentsTree.AfterSelect += ExperimentNodeChanged;
            traitsBox.SelectedIndexChanged += OnTraitsBoxIndexChanged;

            EventManager.EntityNotFound += OnEntityNotFound;
        }        

        #region Form

        private void OnEntityNotFound(object sender, ItemNotFoundArgs args)
        {
            var selector = new ItemSelector(args);
            selector.ShowDialog();
        }

        /// <summary>
        /// For initializing any control properties that cannot be done through the designer
        /// </summary>
        private void InitializeControls()
        {
            pageProperties.AutoScroll = true;
        }

        protected void Close()
        {
            Settings.Instance.Save();
            Logic.TryQueryREMS(new CloseDBCommand(), "The database did not close correctly.");

            experimentsTree.NodeMouseClick -= ExperimentNodeChanged;

            base.Close();
        }

        #endregion

        #region Taskbar

        /// <summary>
        /// On click, prompt the user to create a new blank database
        /// </summary>
        private void MenuNewClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                save.AddExtension = true;
                save.Filter = "SQLite (*.db)|*.db";
                save.RestoreDirectory = true;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    Logic.TryQueryREMS(new CreateDBCommand() { FileName = save.FileName });
                }
            }
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private void MenuOpenClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                open.Filter = "SQLite (*.db)|*.db";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    Logic.TryQueryREMS(new OpenDBCommand() { FileName = open.FileName });
                    LoadTabs();
                }
            }
        }

        /// <summary>
        /// On click, saves changes made to the database
        /// </summary>
        private void MenuSaveClicked(object sender, EventArgs e)
        {
            Settings.Instance.Save();
            Logic.TryQueryREMS(new SaveDBCommand());
        }

        private void MenuSaveAsClicked(object sender, EventArgs e)
        {
            // TODO: Implement
            // string file = ?
            // Logic.TryQueryREMS(new SaveAsDbCommand() { FileName = file });
        }

        /// <summary>
        /// On click, imports data from the selected file
        /// </summary>
        private void MenuImportClicked(object sender, EventArgs e)
        {
            var selector = new FileSelector();

            if (selector.ShowDialog() != DialogResult.OK) return;

            if (!Logic.TryDataImport(selector.InfoTables))
            {
                MessageBox.Show("Information import failed");
                return;
            }

            if (!Logic.TryDataImport(selector.ExpsTables))
            {
                MessageBox.Show("Experiments import failed");
                return;
            }

            if (!Logic.TryDataImport(selector.DataTables))
            {
                MessageBox.Show("Data import failed");
                return;
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        private void MenuExportClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

                if (save.ShowDialog() == DialogResult.OK) Logic.TryDataExport(save.FileName).Wait();
            }
        }

        #endregion       

        #region Tabs

        private void LoadTabs()
        {
            LoadListView();
            
            LoadExperimentsTab();
        }

        #region Information

        private void OnRelationsIndexChanged(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;
            dataGridView.DataSource = Logic.TryQueryREMS(new GetDataTableQuery() { TableName = item });
        }

        private void LoadListView()
        {
            var items = Logic.TryQueryREMS(new GetTableListQuery());

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
            LoadSummaryTab();
            LoadDesignTab();
            LoadCropTab();
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        private void LoadTreeView()
        {
            experimentsTree.Nodes.Clear();

            var exps = Logic.TryQueryREMS(new ExperimentsQuery());

            foreach (var exp in exps)
            {
                TreeNode eNode = new TreeNode(exp.Value) { Tag = new ExperimentTag() { ID = exp.Key } };

                var treatments = Logic.TryQueryREMS(new TreatmentsQuery() { ExperimentId = exp.Key });

                foreach (var treatment in treatments)
                {
                    TreeNode tNode = new TreeNode(treatment.Value)
                    {
                        Tag = new TreatmentTag() { ID = treatment.Key }
                    };
                    tNode.Nodes.Add(new TreeNode("Mean") { Tag = treatment.Key });

                    var plots = Logic.TryQueryREMS(new PlotsQuery() { TreatmentId = treatment.Key });

                    tNode.Nodes.AddRange(plots.Select(p => new TreeNode(p.Value) { Tag = new PlotTag() { ID = p.Key } }).ToArray()) ;
                    eNode.Nodes.Add(tNode);
                }                

                experimentsTree.Nodes.Add(eNode);
            }
            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();
        }
        
        private void LoadSummaryTab()
        {

        }

        private void LoadDesignTab()
        {
            var node = experimentsTree.SelectedNode;
            if (node == null) return;

            var query = new DesignsTableQuery();

            if (node.Tag is TreatmentTag treatment)
            {
                query.TreatmentIds = new int[1] { treatment.ID };
            }
            else if (node.Tag is ExperimentTag experiment)
            {
                var tags = node.Nodes.Cast<TreeNode>().Select(n => n.Tag);
                query.TreatmentIds = tags.Cast<TreatmentTag>().Select(t => t.ID).ToArray();
            }
            else return;

            designData.DataSource = Logic.TryQueryREMS(query);
        }

        private void LoadCropTab()
        {
            // Load the trait type box
            traitTypeBox.Items.Clear();
            var types = Logic.TryQueryREMS(new TraitTypesQuery());
            traitTypeBox.Items.AddRange(types);
            traitTypeBox.SelectedIndex = 0;

            // Load the traits box
            RefreshTraitsBox();

            // Select a node
            experimentsTree.SelectedNode = experimentsTree.Nodes[0];
        }

        private void OnTraitTypeBoxSelectionChanged(object sender, EventArgs e) => RefreshTraitsBox();

        private void OnTraitsBoxIndexChanged(object sender, EventArgs e) => RefreshChart();

        private void ExperimentNodeChanged(object sender, EventArgs e)
        {
            RefreshOperationsData();
            RefreshChart();
        }

        private void RefreshOperationsData()
        {

        }

        private void RefreshTraitsBox()
        {
            traitsBox.Items.Clear();
            var traits = Logic.TryQueryREMS(new TraitsByTypeQuery() { Type = traitTypeBox.SelectedItem.ToString() });
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

        private void RefreshCropData()
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

            foreach (string trait in traitsBox.CheckedItems)
            {
                var bounds = Logic.TryQueryREMS(new PlotDataTraitBoundsQuery() { TraitName = trait });

                if (bounds.YMin < y.Minimum) y.Minimum = bounds.YMin - bounds.YMin / 10;
                if (bounds.YMax > y.Maximum) y.Maximum = bounds.YMax + bounds.YMax / 10;

                var query = new PlotDataByTraitQuery() { TraitName = trait };

                if (node.Tag is ExperimentTag)
                {                    
                    // TODO: Decide what to implement at experiment level
                    // Simply disable crop tab?
                }
                else if (node.Tag is TreatmentTag)
                {
                    foreach (TreeNode plot in node.Nodes)
                    {
                        if (plot.Tag is PlotTag tag)
                        {
                            query.PlotId = tag.ID;

                            PlotSingleData(query, y);
                        }
                    }
                }
                else if (node.Tag is PlotTag plot)
                {
                    query.PlotId = plot.ID;
                    PlotSingleData(query, y);
                }
                else if (node.Text == "Mean")
                {
                    var mean = new MeanTreatmentDataByTraitQuery()
                    {
                        TraitName = trait,
                        TreatmentId = (int)node.Tag
                    };

                    PlotSingleData(mean, y);
                }
            }
            
        }

        private void RefreshSoilData()
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

                            PlotSingleData(query, y);
                        }
                    }
                }
                else if (node.Tag is PlotTag plot)
                {
                    query.PlotId = plot.ID;
                    PlotSingleData(query, y);
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
        private void PlotSingleData(IRequest<SeriesData> query, Axis YAxis)
        {
            var data = Logic.TryQueryREMS(query);

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
                points.DateTimeFormat = "dd-MM";

                line.XValues.DateTime = true;
                line.DateTimeFormat = "dd-MM";
            }

            line.Add(data.X, data.Y);
            points.Add(data.X, data.Y);
            
            cropChart.Series.Add(line);
            cropChart.Series.Add(points);

            line.Color = points.Color;
        }

        
        private void RefreshSoilControl()
        {
            if (!RefreshSoilDataDates()) return;

            leftBtn.Visible = true;
            rightBtn.Visible = true;
            
            RefreshSoilData();            
        }

        

        private DateTime[] dates;
        private int index = 0;
        private bool RefreshSoilDataDates()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return false;


            if (node.Tag is ExperimentTag)
            {
                return false;
            }
            else if (node.Tag is TreatmentTag tag)
            {
                dates = Logic.TryQueryREMS(new SoilLayerDatesQuery() { TreatmentId = tag.ID });
            }
            else
            {
                int id = ((TreatmentTag)node.Parent.Tag).ID;
                dates = Logic.TryQueryREMS(new SoilLayerDatesQuery() { TreatmentId = id });
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
