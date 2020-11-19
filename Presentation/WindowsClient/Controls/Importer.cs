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

        /*
        private List<string> Traits = new List<string>()
        {
            "u",
            "cona",
            "salb",
            "cn_cov",
            "diffus_const",
            "diffus_slope",
            "cn2_bare",
            "cn_red",
            "soil_cn",
            "root_cn",
            "root_wt",
            "enr_a_coeff",
            "enr_b_coeff",
            "LL15",
            "DUL",
            "SW",
            "SAT",
            "BD",
            "SWCON",
            "AirDry",
            "PH",
            "ureappm",
            "NH4N",
            "OC",
            "FBiom",
            "FInert",
            "NO3N",
            "KL",
            "LL",
            "XF",
            "DW1000GR",
            "DWDLF",
            "DWGLF",
            "DWGRAIN",
            "DWHI",
            "DWPANICLE",
            "DWSGLF",
            "DWSGRAIN",
            "DWSPANICLE",
            "DWSSTEM",
            "DWTOTA",
            "LAI",
            "LAW",
            "NMSSTEM",
            "NWDLF",
            "NWGLF",
            "NWGRAIN",
            "NWPANICLE",
            "NWSTEM",
            "NWTOTA",
            "STAGE_NO",
            "FE",
            "SEN",
            "TPLA",
            "SPLA",
            "Rain",
            "Radiation",
            "MaxT",
            "MinT",
            "SW"
        };
        */

        private List<string> tables = new List<string>()
        {
            "Design",
            "HarvestData",
            "PlotData",
            "MetData",
            "SoilLayerData",
            "Irrigation",
            "Fertilization",
            "Soils",
            "SoilLayer",
            "SoilLayers"
        };

        public Importer()
        {
            InitializeComponent();            

            images = new ImageList();
            images.Images.Add("Ignored", Properties.Resources.Ignored);
            images.Images.Add("IgnoredX", Properties.Resources.IgnoredX);
            images.Images.Add("Valid", Properties.Resources.Valid);
            images.Images.Add("Invalid", Properties.Resources.Invalid);
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

            // Prepare individual columns for import
            foreach (var col in table.Columns.Cast<DataColumn>().ToArray())
            {
                // Remove empty columns
                if (col.ColumnName.Contains("Column"))
                {
                    table.Columns.Remove(col);
                    continue;
                }

                // Create a node for the column
                var cnode = new TreeNode(col.ColumnName) { Tag = col };
                
                var info = col.FindProperty();
                col.ExtendedProperties.Add("Info", info);

                if (info is null)
                {
                    if (table.DataSet.Tables["Traits"] is DataTable traits
                        && traits.Columns["Name"] is DataColumn name
                        && traits.Rows.Cast<DataRow>().Any(r => r[name].ToString() == col.ColumnName))
                    {
                        SetState(cnode, "Valid");
                    }
                    // Look for a trait that matches the column in the database
                    else if ((bool)Query(new TraitExistsQuery() { Name = col.ColumnName}).Result)
                    {
                        SetState(cnode, "Valid");
                    }
                    // Default to no trait existing
                    else
                        SetState(cnode, "Invalid");
                }
                else
                {
                    SetState(cnode, "Valid");
                }

                tnode.Nodes.Add(cnode);
            }

            if (tnode.Nodes.Cast<TreeNode>().Any(n => n.ImageKey == "Invalid"))
                SetState(tnode, "Warning");
            else
                SetState(tnode, "Valid");

            return tnode;
        }

        #endregion

        private void SetState(TreeNode node, string key)
        {
            node.ImageKey = key;
            node.SelectedImageKey = key;

            if (node.Tag is DataColumn col)
                col.ExtendedProperties["State"] = key;

            if (node.Tag is DataTable table)
                table.ExtendedProperties["State"] = key;

            stateBox.Image = images.Images[key];
        }

        private void CheckState(TreeNode node)
        {
            if (node.Tag is DataTable table)
            {
                foreach(TreeNode cnode in node.Nodes)
                {
                    var col = cnode.Tag as DataColumn;

                    if (col.ExtendedProperties["State"].ToString() == "Invalid")
                    {
                        SetState(node, "Warning");
                        return;
                    }
                }

                SetState(node, "Valid");
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

                // Find all the infos which are already mapped to a column
                var infos = col.Table.Columns.Cast<DataColumn>()
                    .Select(c => c.ExtendedProperties["Info"])
                    .Where(o => o != null)
                    .Cast<PropertyInfo>();

                bool notEnum(PropertyInfo info)
                {
                    // TODO: Find a better way to test this
                    if (info.PropertyType.Name == typeof(ICollection<>).Name)
                        return false;

                    return true;
                }

                // Find all the non-mapped infos
                var type = col.Table.ExtendedProperties["Type"] as Type;
                var properties = type.GetProperties()
                    .Except(infos)
                    .Where(p => notEnum(p))
                    .Select(p => p.Name)
                    .ToArray();

                propertiesBox.Items.AddRange(properties);
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

            switch (actionBox.SelectedIndex)
            {
                case 0:
                    SetState(dataTree.SelectedNode, "Ignored");
                    propertiesBox.Enabled = false;
                    actionText.Text = "None of the column data will be imported into the database";
                    break;

                case 1:
                    SetState(dataTree.SelectedNode, "Add");
                    propertiesBox.Enabled = false;
                    actionText.Text = "A trait named after the column will be created, and" +
                        "the column values will be mapped to it";
                    break;

                case 2:
                    propertiesBox.Enabled = true;
                    actionText.Text = "Please identify the database property which best matches " +
                        "the column. The column data will be imported using this property.";
                    break;

                default:
                    propertiesBox.Enabled = false;
                    actionText.Text = "Please select an action.";
                    break;
            }

            CheckState(dataTree.SelectedNode.Parent);
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

            SetState(dataTree.SelectedNode, "Valid");

            CheckState(dataTree.SelectedNode.Parent);
        }
    }
}
