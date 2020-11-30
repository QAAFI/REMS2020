using ExcelDataReader;
using Microsoft.EntityFrameworkCore.Internal;

using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Rems.Infrastructure.Excel;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WindowsClient.Forms;
using Rems.Infrastructure.ApsimX;

namespace WindowsClient.Controls
{
    public partial class Importer : UserControl
    {
        public QueryHandler Query { get; set; }

        public event Action DatabaseChanged;

        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private ImageList images;

        public Importer() : base()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            images = new ImageList();
            images.Images.Add("ValidOff", Properties.Resources.ValidOff);
            images.Images.Add("InvalidOff", Properties.Resources.InvalidOff);
            images.Images.Add("ValidOn", Properties.Resources.ValidOn);
            images.Images.Add("InvalidOn", Properties.Resources.InvalidOn);
            images.Images.Add("WarningOn", Properties.Resources.WarningOn);
            images.Images.Add("WarningOff", Properties.Resources.WarningOff);
            images.Images.Add("Add", Properties.Resources.Add);

            dataTree.ImageList = images;            
        }

        #region Data
        public DataSet Data { get; set; } 

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

                table.ExtendedProperties["Ignored"] = false;

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

            if (Is["Override"] is string s && s != "")
                key = s;

            if (Is["Ignored"] is true)
                key += "Off";
            else
                key += "On";

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

                nodeSplitter.Panel2Collapsed = true;

                ignoreBox.Checked = (bool)table.ExtendedProperties["Ignored"];
            }

            else if (e.Node.Tag is DataColumn col)
            {
                importData.DataSource = col.Table;

                nodeSplitter.Panel2Collapsed = false;

                ignoreBox.Checked = (bool)col.ExtendedProperties["Ignored"];

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
                    Data = ReadData(open.FileName);
                    CleanData(Data);

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
                    MessageBox.Show("There errors in the data preventing import.");
                    return;
                }

                var importer = new ExcelImporter();
                importer.Query = Query;
                importer.Data = Data;
                var dialog = new ProgressDialog(importer, "Importing...");
                dialog.TaskComplete += DatabaseChanged;
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                MessageBox.Show(error.Message);
            }
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

        private static Dictionary<string, string> map = new Dictionary<string, string>()
        {
            {"ExpID", "ExperimentId" },
            {"ExpId", "ExperimentId" },
            {"N%", "Nitrogen" },
            {"P%", "Phosphorus" },
            {"K%", "Potassium" },
            {"Ca%", "Calcium" },
            {"S%", "Sulfur" },
            {"Other%", "OtherPercent" }
        };

        private void ReplaceName(DataColumn col)
        {
            if (map.ContainsKey(col.ColumnName))
                col.ColumnName = map[col.ColumnName];
        }

        private void IgnoreBoxCheckChanged(object sender, EventArgs e)
        {
            PropertyCollection items;

            if (dataTree.SelectedNode.Tag is DataColumn col)
                items = col.ExtendedProperties;
            else if (dataTree.SelectedNode.Tag is DataTable table)
                items = table.ExtendedProperties;
            else
                throw new Exception("Invalid node type. Node must represent either a datatable or datacolumn.");

            items["Ignored"] = ignoreBox.Checked;
            isTraitBox.Enabled = !ignoreBox.Checked;
            propertiesBox.Enabled = !ignoreBox.Checked;

            SetState(dataTree.SelectedNode, items);
        }

        private void IsTraitBoxCheckChanged(object sender, EventArgs e)
        {
            propertiesBox.Enabled = !isTraitBox.Checked;

            if (isTraitBox.Checked)
            {
                propertiesBox.SelectedIndex = -1;
            }
        }
    }
}
