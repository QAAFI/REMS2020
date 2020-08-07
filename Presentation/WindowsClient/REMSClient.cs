using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using MediatR;

using Rems.Application;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS.Experiments.Queries.Experiments;
using Rems.Application.DB.Commands;
using Rems.Application.DB.Queries;
using Rems.Application.Entities.Commands;
using Rems.Application.Tables.Queries;
using Rems.Infrastructure;
using Rems.Infrastructure.Excel;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;

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
            InitializeControls();

            experimentsTree.AfterSelect += ExperimentNodeChanged;
            cropTraitsBox.SelectedIndexChanged += OnCropTraitsBoxIndexChanged;

            EventManager.EntityNotFound += OnEntityNotFound;
        }        

        #region Form

        private string OnEntityNotFound(object sender, EntityNotFoundArgs args)
        {
            var selector = new EntitySelector(args.Name, args.Options);
            selector.ShowDialog();
            return selector.Selection;
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

        /// <summary>
        /// On click, imports data from the selected file
        /// </summary>
        private void MenuImportClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = _importFolder != "" ? _importFolder : _importFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    var command = new BulkInsertCommand()
                    {
                        Data = ExcelImporter.ReadRawData(open.FileName),
                        TableMap = Settings.Instance["TABLES"]
                    };

                    if (Logic.TryQueryREMS(command)) MessageBox.Show("Import complete.\n");
                    else MessageBox.Show("Import failed.\n");

                    LoadTabs();
                }
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
            // Load the Traits Box
            var traits = Logic.TryQueryREMS(new TraitsByTypeQuery() { Type = "Crop" });
            cropTraitsBox.Items.AddRange(traits);
            cropTraitsBox.Refresh();

            // Select a node
            experimentsTree.SelectedNode = experimentsTree.Nodes[0];
        }

        private void OnCropTraitsBoxIndexChanged(object sender, EventArgs e)
        {
            RefreshCropData();
        }

        private void ExperimentNodeChanged(object sender, EventArgs e)
        {
            RefreshOperationsData();
            RefreshCropData();
            RefreshSoilData();
        }

        private void RefreshOperationsData()
        {

        }

        private void RefreshCropData()
        {
            cropChart.Series.Clear();

            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            foreach (string trait in cropTraitsBox.CheckedItems)
            {
                var query = new PlotDataByTraitQuery() { TraitName = trait };

                if (node.Tag is ExperimentTag)
                {                    
                    // TODO: Decide what to implement at experiment level
                    // Simply disable crop tab?
                }
                else if (node.Tag is TreatmentTag)
                {
                    PlotRepetitionData(node, query);
                }
                else if (node.Tag is PlotTag plot)
                {
                    query.PlotId = plot.ID;
                    PlotSingleData(query);
                }
                else if (node.Text == "Mean")
                {
                    var mean = new MeanTreatmentDataByTraitQuery()
                    {
                        TraitName = trait,
                        TreatmentId = (int)node.Tag
                    };

                    PlotMeanData(mean);
                }
            }
        }

        /// <summary>
        /// Plot all the repetitions of a treatment for a trait
        /// </summary>
        private void PlotRepetitionData(TreeNode treatment, PlotDataByTraitQuery query)
        {
            foreach (TreeNode plot in treatment.Nodes)
            {
                if (plot.Tag is PlotTag tag)
                {
                    query.PlotId = tag.ID;                    

                    PlotSingleData(query);
                }                
            }
        }

        /// <summary>
        /// Plot a single set of data
        /// </summary>
        private void PlotMeanData(MeanTreatmentDataByTraitQuery query)
        {
            var data = Logic.TryQueryREMS(query);

            if (data is null) return;

            // TODO: It's bad practice to just assume that the data will be
            // of this form, where the rows can be cast directly to DateTime/double. Find a fix?
            var x = data.Select().Select(r => (DateTime)r[0]).ToArray();
            var y = data.Select().Select(r => (double)r[1]).ToArray();

            Points points = new Points();
            Line line = new Line();

            points.Add(x, y);
            line.Add(x, y);

            cropChart.Series.Add(points);
            cropChart.Series.Add(line);
        }

        /// <summary>
        /// Plot a single set of data
        /// </summary>
        private void PlotSingleData(IRequest<DataTable> query)
        {
            var data = Logic.TryQueryREMS(query);

            if (data is null) return;

            // TODO: It's bad practice to just assume that the data will be
            // of this form, where the rows can be cast directly to DateTime/double. Find a fix?
            var x = data.Select().Select(r => (DateTime)r[0]).ToArray();
            var y = data.Select().Select(r => (double)r[1]).ToArray();

            Points points = new Points();
            Line line = new Line();

            points.Add(x, y);
            line.Add(x, y);
            
            cropChart.Series.Add(points);
            cropChart.Series.Add(line);
        }

        private void RefreshSoilData()
        {

        }

        #endregion

        #endregion
    }
}
