using Rems.Infrastructure;
using Rems.Infrastructure.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Forms;
using WindowsClient.Models;
using Settings = WindowsClient.Properties.Settings;

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

        private readonly ExcelReader excel = new();

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

            images.Images.Add("Warning", SystemIcons.Warning);
            images.Images.Add("WarningOn", Properties.Resources.WarningOn);
            images.Images.Add("WarningOff", Properties.Resources.WarningOff);

            images.ImageSize = new System.Drawing.Size(16, 16);

            return images;
        }

        #region Methods

        private async Task GenerateNodes(string format)
        {
            var tables = await Task.Run(() => excel.ConvertData(format));

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

            if (node.Traits.Nodes.Count == 0)
                node.Nodes.Remove(node.Traits);

            node.Refresh();
            return node;
        }

        private string storeformat;
        public async Task OpenFile(string format)
        {
            storeformat = format;

            using var open = new OpenFileDialog();
            open.InitialDirectory = Settings.Default.ImportPath;
            open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";
            
            if (open.ShowDialog() != DialogResult.OK) 
            {
                ImportCancelled?.Invoke(this, EventArgs.Empty); 
                return;
            }

            Parent.Parent.Enabled = false;
            
            Settings.Default.ImportPath = Path.GetDirectoryName(open.FileName);

            if (!await LoadData(open.FileName, format))
                ImportCancelled?.Invoke(this, EventArgs.Empty);

            Parent.Parent.Enabled = true;
        }

        /// <summary>
        /// Lets the user select a file to open for import
        /// </summary>
        /// <returns>True if the file is valid, false otherwise</returns>
        public async Task<bool> LoadData(string file, string format)
        {
            try
            {
                await Task.Run(() => excel.LoadFromFile(file));
                await GenerateNodes(format);

                fileBox.Text = Path.GetFileName(file);

                dataTree.SelectedNode = dataTree.TopNode;

                return true;
            }
            catch (DuplicateNameException e)
            {
                AlertBox.Show(e.Message + "\n" + "Remove duplicate columns in file and try again.", AlertType.Error);
            }
            catch (Exception e)
            {
                AlertBox.Show(e.Message, AlertType.Error);
            }

            return false;
        }

        /// <summary>
        /// Runs the excel importer
        /// </summary>
        private async void RunImporter(object sender, EventArgs args)
        {
            Parent.Parent.Enabled = false;

            if (!FileManager.Connected)
            {
                AlertBox.Show("A database must be opened or created before importing", AlertType.Error);
                return;
            }

            if (excel.Data is null)
            {
                AlertBox.Show("There is no loaded data to import. Please load and validate data.", AlertType.Error);
                return;
            }

            var invalid = dataTree.Nodes.OfType<TableNode>()                    
                .Where(t => t.Valid is false)
                .Any();

            if (invalid)
            {
                AlertBox.Show("All nodes must be valid or ignored before attempting to import", AlertType.Error);
                return;
            }

            // Create and run an importer for the data
            var data = dataTree.Nodes
                .OfType<TableNode>()
                .Where(n => !n.Excel.Ignore)
                .Select(n => n.Excel.Data)
                .OrderBy(t => t.DataSet.Tables.IndexOf(t));

            var inserter = new TableInserter 
            { 
                Data = data,
                Handler = QueryManager.Instance
            };

            tracker.AttachRunner(inserter);

            var task = inserter.Run();
            await task.TryRun();

            if (task.IsCompletedSuccessfully)
            {
                inserter.Dispose();
                excel.Data.Dispose();

                ImportCompleted.Invoke(this, EventArgs.Empty);
            }

            if (task.IsCanceled)
            {
                AlertBox.Show("Import cancelled.", AlertType.Success);
            }

            Reset();
        }

        public void Reset()
        {
            tracker.Reset();
            dataTree.Nodes.Clear();
        }

        #endregion Methods

        #region Event handlers

        /// <summary>
        /// Handles the file button click event
        /// </summary>
        private async void OnFileButtonClicked(object sender, EventArgs e) 
        {
            excel.Data = null;
            dataTree.Nodes.Clear();
            await OpenFile(storeformat);
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
