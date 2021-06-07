using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Rems.Infrastructure.Excel;

using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Enables the import of excel data
    /// </summary>
    public partial class Importer : UserControl
    {
        /// <summary>
        /// Occurs after a file is imported
        /// </summary>
        public event EventHandler ImportCompleted;

        /// <summary>
        /// Occurs when the import is cancelled
        /// </summary>
        public event EventHandler ImportCancelled;

        /// <summary>
        /// If the import does not complete successfully
        /// </summary>
        public event EventHandler ImportFailed;

        /// <summary>
        /// Occurs when the current import stage has changed
        /// </summary>
        public event EventHandler<Args<Stage>> StageChanged;        

        /// <summary>
        /// The excel data
        /// </summary>
        public DataSet Data { get; set; }

        /// <summary>
        /// The system folder most recently accessed by the user
        /// </summary>
        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public Importer() : base()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;            

            dataTree.ImageList = GetTreeImages();

            // Force right click to select node
            dataTree.NodeMouseClick += (s, a) => dataTree.SelectedNode = dataTree.GetNodeAt(a.X, a.Y);
            dataTree.AfterLabelEdit += AfterLabelEdit;

            tracker.TaskBegun += RunImporter;
        }

        private ImageList GetTreeImages()
        {
            // Add icons to the image list
            var images = new ImageList();
            images.Images.Add("ValidOff", Properties.Resources.ValidOff);
            images.Images.Add("InvalidOff", Properties.Resources.InvalidOff);
            images.Images.Add("ValidOn", Properties.Resources.ValidOn);
            images.Images.Add("InvalidOn", Properties.Resources.InvalidOn);
            
            images.Images.Add("Add", Properties.Resources.Add);
            images.Images.Add("Question", Properties.Resources.Question);
            images.Images.Add("Traits", Properties.Resources.Traits);
            images.Images.Add("Excel", Properties.Resources.Excel);
            images.Images.Add("ExcelOff", Properties.Resources.ExcelOff);
            images.Images.Add("Properties", Properties.Resources.Properties);

            images.Images.Add("Warning", SystemIcons.Warning);
            images.Images.Add("WarningOn", Properties.Resources.WarningOn);
            images.Images.Add("WarningOff", Properties.Resources.WarningOff);

            images.ImageSize = new System.Drawing.Size(14, 14);

            return images;
        }

        #region Methods

        /// <summary>
        /// Attempts to sanitise raw excel data so it can be read into the database
        /// </summary>        
        private async Task CleanData(DataSet data)
        {
            dataTree.Nodes.Clear();

            data.FindExperiments();

            foreach (var table in data.Tables.Cast<DataTable>().ToArray())
            {
                // Don't import notes or empty tables
                if (table.TableName == "Notes"
                    || table.Rows.Count == 0
                    || (table.TableName == "Experiments" && table.Columns.Count < 3)
                )
                {
                    data.Tables.Remove(table);
                    continue;
                }

                await CleanTable(table);

                table.ConvertExperiments();

                var node = await CreateTableNode(table);

                dataTree.Nodes.Add(node);
            }
        }

        /// <summary>
        /// Sanitise a single excel table for import
        /// </summary>
        public async Task CleanTable(DataTable table)
        {
            // TODO: This is a quick workaround, find better way to handle unknown tables
            if (table.TableName == "Factors") table.TableName = "Levels";
            if (table.TableName == "Planting") table.TableName = "Sowing";

            table.RemoveDuplicateRows();

            // Find the type of data stored in the table
            var entityType = await QueryManager.Request(new EntityTypeQuery() { Name = table.TableName });
            table.ExtendedProperties["Type"] = entityType ?? throw new Exception("Cannot import unrecognised table: " + table.TableName);

            // Clean columns
            var cols = table.Columns.Cast<DataColumn>().ToArray();
            foreach (var col in cols)
                if (col.ColumnName.Contains("Column"))
                    table.Columns.Remove(col);
                else
                    col.ReplaceName();
        }

        /// <summary>
        /// Validate the contents of a table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private async Task<TreeNode> CreateTableNode(DataTable table)
        {
            var xt = new ExcelTable(table);
            var vt = CreateTableValidater(table);
            var tnode = new TableNode(xt, vt);

            vt.SetAdvice += (s, e) => e.Item.AddToTextBox(adviceBox);

            // Prepare individual columns for import
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var cnode = await vt.CreateColumnNode(i);
                cnode.Updated += (s, e) =>
                {
                    importData.DataSource = cnode.Excel.Source;
                    importData.Format();
                };

                if (cnode.Excel.State["Info"] is not null)
                    tnode.Properties.Nodes.Add(cnode);
                else if (cnode.Excel.State["IsTrait"] is true)
                    tnode.Traits.Nodes.Add(cnode);
                else
                    tnode.Unknowns.Nodes.Add(cnode);
            }

            tnode.Validate();

            return tnode;
        }

        /// <summary>
        /// Generate a validater for a data table
        /// </summary>
        private ITableValidator CreateTableValidater(DataTable table)
        {
            switch (table.TableName)
            {
                case "Design":
                    return new CustomTableValidator(table, new string[] { "Experiment", "Treatment", "Repetition", "Plot" });

                case "HarvestData":
                case "PlotData":
                    return new CustomTableValidator(table, new string[] { "Experiment", "Plot", "Date", "Sample" });

                case "MetData":
                    return new CustomTableValidator(table, new string[] { "MetStation", "Date" });

                case "SoilLayerData":
                    return new CustomTableValidator(table, new string[] { "Experiment", "Plot", "Date", "DepthFrom", "DepthTo" });

                case "Irrigation":
                case "Fertilization":
                case "Tillage":
                    return new CustomTableValidator(table, new string[] { "Experiment", "Treatment" });

                case "Soils":
                case "SoilLayer":
                case "SoilLayers":
                    return new TableValidator(table);

                default:
                    return new TableValidator(table);
            }
        }        

        public async Task OpenFile()
        {
            using var open = new OpenFileDialog();
            open.InitialDirectory = Folder;
            open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

            if (open.ShowDialog() != DialogResult.OK) 
            {
                ImportCancelled?.Invoke(this, EventArgs.Empty); 
                return;
            }

            Folder = Path.GetDirectoryName(open.FileName);

            if (!await LoadData(open.FileName))
                ImportCancelled?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Lets the user select a file to open for import
        /// </summary>
        /// <returns>True if the file is valid, false otherwise</returns>
        public async Task<bool> LoadData(string file)
        {
            try
            {
                Data = await Task.Run(() => ExcelTools.ReadAsDataSet(file));
                await CleanData(Data);

                fileBox.Text = Path.GetFileName(file);

                dataTree.SelectedNode = dataTree.TopNode;

                StageChanged?.Invoke(this, new Args<Stage> { Item = Stage.Validation });

                return true;
            }
            catch (DuplicateNameException e)
            {
                MessageBox.Show(e.Message + "\n" + "Remove duplicate columns in file and try again.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return false;
        }

        /// <summary>
        /// Runs the excel importer
        /// </summary>
        private async void RunImporter(object sender, EventArgs args)
        {
            try
            {
                bool connected = await QueryManager.Request(new ConnectionExists());
                if (!connected)
                {
                    MessageBox.Show("A database must be opened or created before importing");
                    return;
                }

                if (Data is null)
                {
                    MessageBox.Show("There is no loaded data to import. Please load and validate data.");
                    return;
                }

                var states = dataTree.Nodes.Cast<TreeNode>()
                    .Select(n => n.Tag as DataTable)
                    .Where(t => t.ExtendedProperties["Valid"] is false);

                if (states.Any())
                {
                    MessageBox.Show("All nodes must be valid or ignored before attempting to import");
                    return;
                }

                // Create and run an importer for the data
                var excel = new ExcelImporter { Data = Data };

                tracker.AttachRunner(excel);

                excel.TaskFinished += ImportCompleted;
                excel.Query += QueryManager.Request;
                await excel.Run();

                // Clean up
                tracker.Reset();
                excel.Dispose();
                Data.Dispose();

                dataTree.Nodes.ForEach<TableNode>(t =>
                {
                    t.Nodes.ForEach<ColumnNode>(c => c.Dispose());
                    t.Dispose();
                });
                dataTree.Nodes.Clear();

                StageChanged?.Invoke(this, new Args<Stage> { Item = Stage.Imported });
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                MessageBox.Show(error.Message);
                Application.UseWaitCursor = false;

                ImportFailed?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion Methods

        #region Event handlers

        /// <summary>
        /// Handles the file button click event
        /// </summary>
        private async void OnFileButtonClicked(object sender, EventArgs e) 
        {
            await OpenFile();
        }

        /// <summary>
        /// Handles the selection of a new node in the tree
        /// </summary>
        private void TreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            columnLabel.Text = e.Node.Text;

            var node = e.Node;
            while (node.Parent != null) node = node.Parent;

            if (node is not TableNode root)
                return;

            importData.DataSource = root.Excel.Source;
            importData.Format();            

            if (!root.Advice.Empty)
                root.Advice.AddToTextBox(adviceBox);            
        }

        /// <summary>
        /// Handles the renaming of a node in the tree
        /// </summary>
        private void AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node is DataNode<IDisposable, INodeValidator> node && e.Label != null)
            {
                node.Excel.Name = e.Label;
                node.Validate();
            }
        }

        #endregion Event handlers
    }
}
