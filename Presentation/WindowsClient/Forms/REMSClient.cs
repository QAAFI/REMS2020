using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using Rems.Infrastructure.ApsimX;
using Rems.Infrastructure.Excel;
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
        private string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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

            EventManager.ItemNotFound += exportValidater.HandleMissingItem;
            EventManager.SendQuery += QueryREMS;

            exportValidater.SendQuery = new QueryHandler(QueryREMS);
            importValidater.SendQuery = new QueryHandler(QueryREMS);
        }

        #region Taskbar

        /// <summary>
        /// On click, prompt the user to create a new blank database
        /// </summary>
        private async void MenuNewClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = folder;
                save.AddExtension = true;
                save.Filter = "SQLite (*.db)|*.db";
                save.RestoreDirectory = true;

                if (save.ShowDialog() != DialogResult.OK) return;
                
                folder = Path.GetDirectoryName(save.FileName);

                await TryQueryREMS(new CreateDBCommand() { FileName = save.FileName });
                await TryQueryREMS(new OpenDBCommand() { FileName = save.FileName });

                LoadListView();                
            }
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private async void MenuOpenClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = folder;
                open.Filter = "SQLite (*.db)|*.db";

                if (open.ShowDialog() != DialogResult.OK) return;
                
                folder = Path.GetDirectoryName(open.FileName);

                await TryQueryREMS(new OpenDBCommand() { FileName = open.FileName });

                UpdateAllComponents();                
            }
        }

        private async void MenuImportClicked(object sender, EventArgs e)
        {
            Enabled = false;

            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = folder;
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() != DialogResult.OK) return;
                
                folder = Path.GetDirectoryName(open.FileName);

                try
                {
                    if (!await TryQueryREMS(new ConnectionExists()))
                    {
                        MessageBox.Show("A database must be opened or created before importing");
                        return;
                    }

                    var importer = new ExcelImporter(QueryREMS, TryQueryREMS);
                    importer.FoundInvalids += importValidater.OnFoundInvalids;
                    
                    if (importer.Initialise(open.FileName))
                    {
                        var dialog = new ProgressDialog(importer, "Importing...");
                        dialog.TaskComplete += UpdateAllComponents;
                    }                    
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

            Enabled = true;
        }        

        private void UpdateAllComponents()
        {
            LoadListView();
            LoadTreeView();
            traitChart.LoadTraitsBox();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void MenuExportClicked(object sender, EventArgs e)
        {
            if (!await TryQueryREMS(new ConnectionExists()))
            {
                MessageBox.Show("A database must be opened before exporting.");
                return;
            }

            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = folder;
                save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

                if (save.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var exporter = new ApsimXporter(QueryREMS, TryQueryREMS)
                    {
                        FileName = save.FileName
                    };
                    var dialog = new ProgressDialog(exporter, "Exporting...");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        public Task<object> QueryREMS(object request)
        {
            return _mediator.Send(request);
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

        public string ParseText(string file)
        {
            var filePath = Path.Combine("DataFiles", "apsimx", file);
            StringBuilder builder = new StringBuilder();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream) builder.AppendLine(reader.ReadLine());
            }

            builder.Replace("\r", "");
            return builder.ToString();
        }
        #endregion
    }
}
