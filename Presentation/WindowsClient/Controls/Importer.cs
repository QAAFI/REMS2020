using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsClient.Models;
using WindowsClient.Utilities;

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
            images.Images.Add("Question", SystemIcons.Question);
            images.Images.Add("Traits", Properties.Resources.Traits);
            images.Images.Add("Excel", Properties.Resources.Excel);
            images.Images.Add("ExcelOff", Properties.Resources.ExcelOff);
            images.Images.Add("Properties", Properties.Resources.Properties);
            images.Images.Add("Ignore", Properties.Resources.Ignore);

            images.Images.Add("Warning", SystemIcons.Warning);
            images.Images.Add("WarningOn", Properties.Resources.WarningOn);
            images.Images.Add("WarningOff", Properties.Resources.WarningOff);

            images.ImageSize = new System.Drawing.Size(16, 16);

            return images;
        }

        #region Methods

        private async Task GenerateNodes(DataSet data, string format)
        {
            var query = new ExcelDataQuery { Data = data, Format = format };
            var tables = await QueryManager.Request(query);

            foreach (var pair in tables)
            {
                TreeNode node;

                if (pair.Key.Data is not null)
                    node = await GenerateTableNode(pair.Key, pair.Value);
                else
                    node = new InvalidNode(pair.Key);

                if (node is not null)
                    dataTree.Nodes.Add(node);
            }
        }

        private async Task<TableNode> GenerateTableNode(ExcelTable table, ExcelColumn[] columns)
        {
            var node = new TableNode(table);

            if (table.Type is null) return null;

            // Add expected nodes
            foreach (var col in columns)
            {
                var cn = new RequiredNode(col);
                node.Required.Nodes.Add(cn);
            }

            // Add unknown nodes
            var unknowns = table.Data.Columns
                .OfType<DataColumn>()
                .Except(columns.Select(x => x.Data));

            foreach (var col in unknowns)
            {
                var excel = new ExcelColumn { Data = col };
                var cn = new TraitNode(excel);           

                node.Traits.Nodes.Add(cn);
                await cn.Initialise();
            }

            node.Refresh();
            return node;
        }

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

        public async Task OpenFile(string format)
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

            if (!await LoadData(open.FileName, format))
                ImportCancelled?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Lets the user select a file to open for import
        /// </summary>
        /// <returns>True if the file is valid, false otherwise</returns>
        public async Task<bool> LoadData(string file, string format)
        {
            try
            {
                Data = await Task.Run(() => ExcelTools.ReadAsDataSet(file));
                await CleanData(Data);
                await GenerateNodes(Data, format);

                fileBox.Text = Path.GetFileName(file);

                dataTree.SelectedNode = dataTree.TopNode;

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

            var invalid = dataTree.Nodes.OfType<TableNode>()                    
                .Where(t => t.Valid is false)
                .Any();

            if (invalid)
            {
                MessageBox.Show("All nodes must be valid or ignored before attempting to import");
                return;
            }

            // Create and run an importer for the data
            var data = dataTree.Nodes
                .OfType<TableNode>()
                .Where(n => !n.Excel.Ignore)
                .Select(n => n.Excel.Data)
                .OrderBy(t => t.DataSet.Tables.IndexOf(t));

            var excel = new ExcelImporter { Data = data };

            tracker.AttachRunner(excel);

            var task = excel.Run();
            await task.TryRun();

            if (task.IsCompletedSuccessfully)
            {
                excel.Dispose();
                Data.Dispose();

                dataTree.Nodes.ForEach<TableNode>(t =>
                {
                    t.Nodes.ForEach<RequiredNode>(c => c.Dispose());
                    t.Dispose();
                });
                dataTree.Nodes.Clear();

                ImportCompleted.Invoke(this, EventArgs.Empty);
            }

            tracker.Reset();            
        }

        #endregion Methods

        #region Event handlers

        /// <summary>
        /// Handles the file button click event
        /// </summary>
        private async void OnFileButtonClicked(object sender, EventArgs e) 
        {
            //await OpenFile();
        }

        /// <summary>
        /// Handles the selection of a new node in the tree
        /// </summary>
        private void TreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            warning.Visible = false;
            importData.Visible = true;
            var node = e.Node;
            int selected = -1;
            
            if (node is RequiredNode required)
            {
                if (warning.Visible = !required.Valid)
                {
                    importData.Visible = false;
                    warninglabel.Text = "Missing required columns. Please include the specified columns " +
                        "in your excel file before continuing.";
                    return;
                }

                selected = required.Excel.Data.Ordinal;
                required.Advice?.AddToTextBox(adviceBox);
                UpdateGrid(required.Excel.Source);
            }
            else if (node is TraitNode column)
            {
                selected = column.Excel.Data.Ordinal;
                column.Advice?.AddToTextBox(adviceBox);
                UpdateGrid(column.Excel.Source);
            }
            else if (node is GroupNode group)
            {
                var table = group.Parent as TableNode;
                group.Advice?.AddToTextBox(adviceBox);
                UpdateGrid(table.Excel.Source);
            }
            else if (node is TableNode table)
            {
                table.Advice?.AddToTextBox(adviceBox);
                UpdateGrid(table.Excel.Source);
            }
            else if (node is InvalidNode invalid)
            {
                importData.Visible = false;
                
                string text = $"REMS looked for a {invalid.Excel.Type.Name} table but did not find one. ";

                text += (invalid.Excel.Required)
                    ? "This data is required for the import process. Please update the spreadsheet and try again."
                    : "This data is non-essential and this node can be safely ignored to continue the import.";

                warning.Visible = true;
                warninglabel.Text = text;
            }

            importData.Format(selected);
        }

        private void UpdateGrid(object source)
        {
            if (source is DataTable table)
            {
                gridLabel.Text = table.TableName;
                importData.DataSource = table;
            }
        }

        /// <summary>
        /// Handles the renaming of a node in the tree
        /// </summary>
        private void AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            
        }

        #endregion Event handlers
    }
}
