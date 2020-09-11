using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Models.Core.ApsimFile;
using Rems.Application;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using Rems.Infrastructure;
using Rems.Infrastructure.ApsimX;
using Rems.Infrastructure.Excel;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;
using WindowsClient.Forms;

namespace WindowsClient
{
    public enum TagType
    {
        Empty,
        Experiment,
        Treatment,
        Plot
    }

    public struct NodeTag
    {
        public int ID { get; set; }

        public TagType Type { get; set; }
    }

    public partial class REMSClient : Form
    {
        private string _importFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "Data");

        private readonly IMediator _mediator;

        public REMSClient(IServiceProvider provider)
        {
            InitializeComponent();

            _mediator = provider.GetRequiredService<IMediator>();
            
            operationsBox.SelectedIndex = 0;

            traitChart.SeriesQuery += TryQueryREMS;
            traitChart.BoundsQuery += TryQueryREMS;
            traitChart.DatesQuery += TryQueryREMS;
            traitChart.StringsQuery += TryQueryREMS;

            experimentsTree.AfterSelect += OnExperimentNodeChanged;

            EventManager.ItemNotFound += OnEntityNotFound;
            EventManager.ProgressTracking += OnProgressTrackingActivated;            
        }

        #region Form

        private void OnEntityNotFound(object sender, ItemNotFoundArgs args)
        {
            var selector = new ItemSelector(args);
            selector.ShowDialog();
        }

        private void OnProgressTrackingActivated(object sender, ProgressTrackingArgs args)
        {
            // TODO: Data import needs to be refactored before we can disable the main client during import.
            // This is due to threading issues

            //Enabled = false;

            var tracker = new ProgressDialog(args.Title, args.Items);
            tracker.Show();
        }

        protected new async void Close()
        {
            await TryQueryREMS(new CloseDBCommand());

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
                    await TryQueryREMS(new CreateDBCommand() { FileName = save.FileName });
                    await TryQueryREMS(new OpenDBCommand() { FileName = save.FileName });

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
                    await TryQueryREMS(new OpenDBCommand() { FileName = open.FileName });
                    LoadListView();

                    LoadExperimentsTab();

                    traitChart.LoadTraitsBox();
                }
            }
        }

        /// <summary>
        /// On click, saves changes made to the database
        /// </summary>
        private void MenuSaveClicked(object sender, EventArgs e)
        {
            
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
            Import();
        }

        private void ImportExperimentsClicked(object sender, EventArgs e)
        {
            // Even though the import operation is the same, we make a distinction between
            // information, experiments and data for the users sake 
            Import();
        }

        private void ImportDataClicked(object sender, EventArgs e)
        {
            // Even though the import operation is the same, we make a distinction between
            // information, experiments and data for the users sake 
            Import();
        }

        private void Import()
        {
            Enabled = false;
            ImportFile();
            EventManager.InvokeStopProgress(null, EventArgs.Empty);
            Enabled = true;
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
                        var importer = new ExcelImporter(_mediator);
                        var data = importer.ReadDataSet(open.FileName);
                        importer.InsertDataSet(data);

                        MessageBox.Show("Import complete!");

                        LoadExperimentsTab();
                    }
                    catch (IOException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                    catch (Exception error)
                    {
                        while (error.InnerException != null) error = error.InnerException;
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

                if (save.ShowDialog() == DialogResult.OK) 
                    await Task.Run(() => TryDataExport(save.FileName));
            }
        }

        #endregion       

        #region Tabs

        #region Information

        private async void OnRelationsIndexChanged(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;
            dataGridView.DataSource = await TryQueryREMS(new DataTableQuery() { TableName = item });
        }

        private async void LoadListView()
        {
            var items = await TryQueryREMS(new GetTableListQuery());

            relationsListBox.Items.Clear();
            relationsListBox.Items.AddRange(items.ToArray());
        }

        #endregion

        #region Experiments

        

        /// <summary>
        /// Process the currently selected experiments tab
        /// </summary>
        private void LoadExperimentsTab()
        {
            LoadTreeView();
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        private async void LoadTreeView()
        {
            experimentsTree.Nodes.Clear();

            var exps = await TryQueryREMS(new ExperimentsQuery());

            foreach (var exp in exps)
            {
                TreeNode eNode = new TreeNode(exp.Value) { Tag = new NodeTag() { ID = exp.Key, Type = TagType.Experiment } };

                var treatments = await TryQueryREMS(new TreatmentsQuery() { ExperimentId = exp.Key });

                foreach (var treatment in treatments)
                {
                    TreeNode tNode = new TreeNode(treatment.Value)
                    {
                        Tag = new NodeTag() { ID = treatment.Key, Type = TagType.Treatment }
                    };
                    tNode.Nodes.Add(new TreeNode("All") { Tag = new NodeTag() { Type = TagType.Empty } });

                    var plots = await TryQueryREMS(new PlotsQuery() { TreatmentId = treatment.Key });

                    tNode.Nodes.AddRange(plots.Select(p =>
                    {
                        var tag = new NodeTag() { ID = p.Key, Type = TagType.Plot };
                        return new TreeNode(p.Value) { Tag = tag };
                    }).ToArray());
                    eNode.Nodes.Add(tNode);
                }                

                experimentsTree.Nodes.Add(eNode);
            }
            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();
        }

        private void OnExperimentNodeChanged(object sender, EventArgs e)
        {
            traitChart.Node = experimentsTree.SelectedNode;

            RefreshSummary();
            RefreshOperationsData();
        }

        private void OnOperationsBoxSelectionChanged(object sender, EventArgs e)
        {
            RefreshOperationsData();
        }

        private async void RefreshSummary()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            if (node.Tag is NodeTag tag && tag.Type == TagType.Experiment)
            {
                var query = new ExperimentSummary() { ExperimentId = tag.ID };

                var experiment = await TryQueryREMS(query);

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

                var sowing = await TryQueryREMS(new SowingSummary() { ExperimentId = tag.ID });
                sowingMethodBox.Content = sowing["Method"];
                sowingDateBox.Content = sowing["Date"];
                sowingDepthBox.Content = sowing["Depth"];
                sowingRowBox.Content = sowing["Row"];
                sowingPopBox.Content = sowing["Pop"];

                var design = new DesignsTableQuery() { ExperimentId = tag.ID };
                designData.DataSource = await TryQueryREMS(design);
            }
        }

        private async void RefreshOperationsData()
        {
            var node = experimentsTree.SelectedNode;
            if (node is null) return;

            var tag = (NodeTag)node.Tag;
            if (tag.Type is TagType.Experiment) return;                 

            IRequest<SeriesData> query;

            string item = operationsBox.SelectedItem?.ToString();
            if (item == "Irrigations")
                query = new IrrigationDataQuery() { TreatmentId = tag.ID };
            else if (item == "Fertilizations")
                query = new FertilizationDataQuery() { TreatmentId = tag.ID };
            else if (item == "Tillages")
                query = new TillagesDataQuery() { TreatmentId = tag.ID };
            else
                return;

            var data = await TryQueryREMS(query);

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

        #endregion

        #endregion

        #region Logic

        private async Task<bool> TryDataExport(string file)
        {
            try
            {
                IApsimX apsim = new ApsimX(_mediator);
                var sims = await apsim.CreateModels();
                File.WriteAllText(file, FileFormat.WriteToString(sims));

                //MessageBox.Show($"Export Complete.");
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public Task<T> TryQueryREMS<T>(IRequest<T> request)
        {
            Application.UseWaitCursor = true;

            List<Exception> errors = new List<Exception>();

            try
            {
                var task = _mediator.Send(request);

                Application.UseWaitCursor = false;
                return task;
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                errors.Add(error);
            }

            var builder = new StringBuilder();

            foreach (var error in errors)
            {
                builder.AppendLine(error.Message + "at\n" + error.StackTrace + "\n");
            }

            MessageBox.Show(builder.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.UseWaitCursor = false;

            return Task.Run(() => default(T));
        }

        #endregion
    }
}
