using ExcelDataReader;
using Microsoft.EntityFrameworkCore.Internal;

using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Rems.Infrastructure.Excel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

using WindowsClient.Forms;

namespace WindowsClient.Controls
{
    public partial class Importer : UserControl
    {
        public QueryHandler Query;

        public event Action DatabaseChanged;

        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private ExcelImporter importer;

        private ImageList images;

        public Importer()
        {
            InitializeComponent();            

            images = new ImageList();
            images.Images.Add("ValidOff", Properties.Resources.ValidOff);
            images.Images.Add("InvalidOff", Properties.Resources.InvalidOff);
            images.Images.Add("ValidOn", Properties.Resources.ValidOn);
            images.Images.Add("InvalidOn", Properties.Resources.InvalidOn);
            images.Images.Add("Warning", Properties.Resources.Warning);
            images.Images.Add("Add", Properties.Resources.Add);

            dataTree.ImageList = images;
        }

        public void Initialise()
        {
            importer = new ExcelImporter(Query);
            importer.ItemNotFound += Importer_ItemNotFound;
        }

        private string Importer_ItemNotFound(string item)
        {
            throw new NotImplementedException();
        }

        #region Data
        private DataSet ReadData(string filepath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                }
            }
        }

        private void CleanData(DataSet data)
        {
            dataTree.Nodes.Clear();

            foreach (var table in data.Tables.Cast<DataTable>().ToArray())
            {   
                if (table.TableName == "Notes" || table.Rows.Count == 0)
                {
                    data.Tables.Remove(table);
                    continue;
                }

                // TODO: This is a quick workaround, find better way to handle factors/levels table
                if (table.TableName == "Factors") table.TableName = "Levels";

                // TODO: This is a quick workaround, find better way to handle planting/sowing table
                if (table.TableName == "Planting") table.TableName = "Sowing";

                // Remove any duplicate rows from the table
                table.RemoveDuplicateRows();
                
                var type = Query(new EntityTypeQuery() { Name = table.TableName }).Result;
                if (type == null) throw new Exception("Cannot import unrecognised table: " + table.TableName);

                table.ExtendedProperties.Add("Type", type);
                
                dataTree.Nodes.Add(ValidateTable(table));
            }            
        }

        private TreeNode ValidateTable(DataTable table)
        {
            var tnode = new TreeNode(table.TableName) { Tag = table };
            table.ExtendedProperties["Valid"] = true;

            bool valid = true;
            // Prepare individual columns for import
            foreach (var col in table.Columns.Cast<DataColumn>().ToArray())
            {
                // Remove empty columns
                if (col.ColumnName.Contains("Column"))
                {
                    table.Columns.Remove(col);
                    continue;
                }

                col.ExtendedProperties["Ignored"] = false;

                // Use some default name replacement options
                ReplaceName(col);

                // Create a node for the column
                var cnode = new TreeNode(col.ColumnName) { Tag = col };
                
                var info = col.FindProperty();
                col.ExtendedProperties["Info"] = info;

                // Don't question it
                if (info is null && 
                    (
                        (bool)Query(new TraitExistsQuery() { Name = col.ColumnName }).Result
                        || table.DataSet.Tables["Traits"] is DataTable traits
                        && traits.Columns["Name"] is DataColumn name
                        && traits.Rows.Cast<DataRow>().Any(r => r[name].ToString() == col.ColumnName))
                    )
                {
                    col.ExtendedProperties["Valid"] = true;
                }

                tnode.Nodes.Add(cnode);
                SetState(cnode, col.ExtendedProperties);

                valid &= (bool)col.ExtendedProperties["Valid"];
            }

            if (!valid) 
                table.ExtendedProperties["Override"] = "Warning";

            SetState(tnode, table.ExtendedProperties);

            return tnode;
        }

        #endregion

        private void SetState(TreeNode node, PropertyCollection Is)
        {
            string key = "";

            if (Is["Valid"] is true)
                key += "Valid";
            else
                key += "Invalid";         

            if (Is["Ignored"] is true)
                key += "Off";
            else
                key += "On";

            if (Is["Override"] is string s && s != "")
                key = s;

            node.ImageKey = key;
            node.SelectedImageKey = key;

            stateBox.Image = images.Images[key];

            // Update the node parent
            CheckState(node.Parent);
        }

        private void CheckState(TreeNode node)
        {
            if (node?.Tag is DataTable table)
            {
                var cols = table.Columns.Cast<DataColumn>();
                if (cols.Any(c => c.ExtendedProperties["Valid"] is false && c.ExtendedProperties["Ignored"] is false))
                    table.ExtendedProperties["Override"] = "Warning";
                else
                    table.ExtendedProperties["Override"] = "";

                SetState(node, table.ExtendedProperties);
            }
        }

        private void TreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is DataTable table)
            {
                importData.DataSource = table;

                nodeSplitter.Panel1Collapsed = true;
            }

            else if (e.Node.Tag is DataColumn col)
            {
                importData.DataSource = col.Table;

                nodeSplitter.Panel1Collapsed = false;

                if (col.ExtendedProperties["Action"] is int i)
                    actionBox.SelectedIndex = i;
                else
                    actionBox.SelectedIndex = -1;

                // Reset the available properties
                propertiesBox.Items.Clear();

                var items = col.GetUnmappedProperties()
                    .Select(p => p.Name)
                    .ToArray();

                propertiesBox.Items.AddRange(items);
            }

            stateBox.Image = images.Images[e.Node.ImageKey];
            columnLabel.Text = e.Node.Text;
        }

        private void OnLoadClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Folder;
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() != DialogResult.OK) return;

                Folder = Path.GetDirectoryName(open.FileName);

                try
                {
                    importer.Data = ReadData(open.FileName);
                    CleanData(importer.Data);

                    fileBox.Text = Path.GetFileName(open.FileName);

                    dataTree.SelectedNode = dataTree.TopNode;
                }
                catch (IOException error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private async void OnImportClicked(object sender, EventArgs e)
        {
            try
            {
                bool connected = (bool)await Query(new ConnectionExists());
                if (!connected)
                {
                    MessageBox.Show("A database must be opened or created before importing");
                    return;
                }

                var states = dataTree.Nodes.Cast<TreeNode>()
                    .Select(n => n.Tag as DataTable)
                    .Where(t => t.ExtendedProperties["State"].ToString() != "Valid");

                if (states.Any())
                {
                    MessageBox.Show("Cannot import invalid data");
                    return;
                }

                var dialog = new ProgressDialog(importer, "Importing...");
                dialog.TaskComplete += DatabaseChanged;
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                MessageBox.Show(error.Message);
            }
        }

        private void ActionBoxSelectionChanged(object sender, EventArgs e)
        {
            var col = dataTree.SelectedNode.Tag as DataColumn;
            col.ExtendedProperties["Action"] = actionBox.SelectedIndex;

            void setValues(bool ignored, bool enabled, string action)
            {
                col.ExtendedProperties["Ignored"] = ignored;
                SetState(dataTree.SelectedNode, col.ExtendedProperties);
                propertiesBox.Enabled = enabled;
                actionText.Text = action;
            }

            string text;

            switch (actionBox.SelectedIndex)
            {
                case 0:
                    text = "None of the column data will be imported into the database";
                    setValues(true, false, text);
                    break;

                case 1:
                    text = "A trait named after the column will be created, and" +
                        "the column values will be mapped to it";
                    setValues(false, false, text);
                    break;

                case 2:
                    text = "Please identify the database property which best matches " +
                        "the column. The column data will be imported using this property.";
                    setValues(false, true, text);
                    break;

                default:
                    text = "Please select an action.";
                    setValues(false, false, text);
                    break;
            }

            SetState(dataTree.SelectedNode, col.ExtendedProperties);
        }

        private void PropertiesSelectionChanged(object sender, EventArgs e)
        {
            var item = propertiesBox.SelectedItem.ToString();
            var col = (DataColumn)dataTree.SelectedNode.Tag;

            if (col.Table.Columns.Contains(item))
            {
                MessageBox.Show("The table already has a column mapped to this property");
                return;
            }

            col.ColumnName = item;
            col.ExtendedProperties["Valid"] = true;

            SetState(dataTree.SelectedNode, col.ExtendedProperties);            
        }

        private void ReplaceName(DataColumn col)
        {
            switch (col.ColumnName)
            {
                case "ExpID":
                    col.ColumnName = "ExperimentId"; return;

                case "N%":
                    col.ColumnName = "Nitrogen"; return;

                case "P%":
                    col.ColumnName = "Phosphorus"; return;

                case "K%":
                    col.ColumnName = "Potassium"; return;

                case "Ca%":
                    col.ColumnName = "Calcium"; return;

                case "S%":
                    col.ColumnName = "Sulfur"; return;

                case "Other%":
                    col.ColumnName = "OtherPercent"; return;

                default:
                    return;
            }
        }
    }
}
