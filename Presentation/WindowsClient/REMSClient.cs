using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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

            FormClosed += REMSClientFormClosed;
            //tablesBox.Click += UpdatePageDisplay;
            notebook.SelectedIndexChanged += UpdatePageDisplay;
            Logic.ListViewOutdated += UpdateListView;

            experimentsTree.AfterSelect += ExperimentNodeChanged;            

            EventManager.EntityNotFound += OnEntityNotFound;
        }

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

        private void REMSClientFormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Instance.Save();
            Logic.TryQueryREMS(new CloseDBCommand(), "The database did not close correctly.");

            FormClosed -= REMSClientFormClosed;
            //tablesBox.Click -= UpdatePageDisplay;
            notebook.SelectedIndexChanged -= UpdatePageDisplay;
            Logic.ListViewOutdated -= UpdateListView;

            experimentsTree.NodeMouseClick -= ExperimentNodeChanged;
        }

        #region Tabs

        private void UpdatePageDisplay(object sender, EventArgs e)
        {
            // TODO: Clean up this mess

            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;

            if (notebook.SelectedTab == pageInfo)
            {
                dataGridView.DataSource = Logic.TryQueryREMS(new GetDataTableQuery() { TableName = item });
            }
            else if (notebook.SelectedTab == pageProperties)
            {
                item = item.Remove(item.Length - 1);

                pageProperties.Controls.Clear();
                pageProperties.Controls.AddRange(Logic.GetProperties(item));
            }
            else if (notebook.SelectedTab == pageGraph)
            {

            }
            else if (notebook.SelectedTab == pageExps)
            {
                UpdateTreeView();
                UpdateExperimentsTab();
            }
        }

        #region Information

        private void UpdateListView(object sender, EventArgs e)
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

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        private void UpdateTreeView()
        {
            var exps = Logic.TryQueryREMS(new ExperimentsQuery());

            foreach (var exp in exps)
            {
                var treatments = Logic.TryQueryREMS(new TreatmentsQuery() { ExperimentId = exp.Key });

                TreeNode node = new TreeNode(exp.Value) { Tag = new ExperimentTag() { ID = exp.Key } };

                var nodes = treatments.Select(t => new TreeNode(t.Value) { Tag = new TreatmentTag() { ID = t.Key } }).ToArray();
                node.Nodes.AddRange(nodes);

                experimentsTree.Nodes.Add(node);
            }
            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();
        }

        private void ExperimentNodeChanged(object sender, EventArgs e)
        {
            switch (experimentsTab.SelectedTab.Text)
            {
                case "Summary":
                    UpdateSummaryTab();
                    return;

                case "Design":
                    UpdateDesignTab();
                    return;

                case "Operations":
                    UpdateOperationsTab();
                    return;

                case "Crop":
                    UpdateCropTab();
                    return;

                case "Soil":
                    UpdateSoilTab();
                    return;

                default:
                    return;
            }
        }

        private void UpdateSummaryTab()
        {

        }

        private void UpdateDesignTab()
        {
            var query = new DesignsTableQuery();

            var node = experimentsTree.SelectedNode;

            if (node.Tag is TreatmentTag treatment)
            {
                query.TreatmentIds = new int[1] { treatment.ID };
            }
            else if (node.Tag is ExperimentTag experiment)
            {
                var tags = node.Nodes.Cast<TreeNode>().Select(n => n.Tag);
                query.TreatmentIds = tags.Cast<TreatmentTag>().Select(t => t.ID).ToArray();
            }

            designData.DataSource = Logic.TryQueryREMS(query);
        }

        private void UpdateOperationsTab()
        {

        }

        private void UpdateCropTab()
        {
            var traits = Logic.TryQueryREMS(new TraitsByTypeQuery() { Type = "Crop" });

            cropTraitsBox.Items.AddRange(traits);

            cropTraitsBox.Refresh();
        }

        private void UpdateSoilTab()
        {

        }

        /// <summary>
        /// Process the currently selected experiments tab
        /// </summary>
        private void UpdateExperimentsTab()
        {

        }

        #endregion

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
                    //Logic.LoadSettings();
                    UpdateListView(null, EventArgs.Empty);
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
                    //Logic.LoadSettings();
                    UpdateListView(null, EventArgs.Empty);
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

        #region Graph

        private void GraphTableChanged(object sender, EventArgs e)
        {
            var table = comboTable.SelectedItem.ToString();
            var names = Logic.TryQueryREMS(new GetTraitNamesByIdQuery() { TraitIds = table });

            comboTrait.Items.Clear();
            comboTrait.Items.AddRange(names);

            var items = Logic.TryQueryREMS(new GetGraphableItemsQuery() { TableName = table });

            comboXData.Items.Clear();
            comboXData.Items.AddRange(items);

            comboYData.Items.Clear();
            comboYData.Items.AddRange(items);
        }

        private void GraphTraitChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void XDataChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void YDataChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            var items = new string[4];
            items[0] = comboTable.SelectedItem?.ToString();
            items[1] = comboTrait.SelectedItem?.ToString();
            items[2] = comboXData.SelectedItem?.ToString();
            items[3] = comboYData.SelectedItem?.ToString();

            if (!items.Any(n => n == null))
            {
                var query = new GetGraphDataQuery()
                {
                    TableName = items[0],
                    TraitName = items[1],
                    XColumn = items[2],
                    YColumn = items[3]
                };

                var data = Logic.TryQueryREMS(query);

                var p = new Points();
                p.Color = Color.SkyBlue;
                p.Legend.Visible = false;
                p.Pointer.Style = PointerStyles.Circle;

                var l = new Line();
                l.Color = Color.LightSkyBlue;
                l.Legend.Visible = false;
                l.LinePen.Width = 2;

                foreach (var t in data)
                {
                    CastAdd(p, t.Item1, t.Item2);
                    CastAdd(l, t.Item1, t.Item2);
                }

                graph.Series.Clear();
                graph.Series.Add(p);
                graph.Series.Add(l);
            }
        }

        private void CastAdd(CustomPoint point, object x, object y)
        {
            if (x is DateTime a && y is DateTime b) point.Add(a, b);
            else if (x is double c && y is DateTime d) point.Add(c, d);
            else if (x is DateTime e && y is double f) point.Add(e, f);
            else point.Add(Convert.ToDouble(x), Convert.ToDouble(y));
        }

        #endregion

    }
}
