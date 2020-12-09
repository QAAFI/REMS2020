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
using WindowsClient.Models;

namespace WindowsClient.Controls
{    
    public partial class Importer : UserControl
    {
        public QueryHandler Query { get; set; }

        public event Action FileImported;
        public event Action<Stage> StageChanged;
        public event Action<string> FileChanged;

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
            images.ImageSize = new System.Drawing.Size(14, 14);

            dataTree.ImageList = images;
            
            // Force right click to select node
            dataTree.NodeMouseClick += (s, a) => dataTree.SelectedNode = dataTree.GetNodeAt(a.X, a.Y);

            tracker.TaskBegun += TrackerTaskBegun;
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
            var tnode = new DataNode(table) { Query = Query };

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

                // Use some default name replacement options
                ReplaceName(col);

                // Create a node for the column
                var cnode = new DataNode(col) { Query = Query };

                var info = col.FindProperty();
                col.ExtendedProperties["Info"] = info;
                col.ExtendedProperties["Ignore"] = false;

                // Test if a column without a matching property is a trait
                bool isTrait()
                {
                    return
                    // Test if the trait is in the database
                    (bool)Query(new TraitExistsQuery() { Name = col.ColumnName }).Result
                    // Or in the spreadsheet traits table
                    || table.DataSet.Tables["Traits"] is DataTable traits
                    // If it is, find the column of trait names
                    && traits.Columns["Name"] is DataColumn name
                    // 
                    && traits.Rows.Cast<DataRow>().Any(r => r[name].ToString() == col.ColumnName);
                };

                if (info is null && !isTrait())
                    cnode.UpdateState("Valid", false);
                else
                    cnode.UpdateState("Valid", true);
                tnode.Nodes.Add(cnode);

                valid &= (bool)col.ExtendedProperties["Valid"];
            }

            if (valid)
                tnode.UpdateState("Valid", true);     
            else
                tnode.UpdateState("Override", "Warning");

            return tnode;
        }

        #endregion

        private void TreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is DataNode node)
            {
                importData.DataSource = node.Source;
                columnLabel.Text = node.Text;
                
                adviceBox.Clear();
                adviceBox.AddText(node.Advice);
            }
        }

        public bool SelectFile()
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Folder;
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() != DialogResult.OK) return false;

                Folder = Path.GetDirectoryName(open.FileName);

                try
                {                    
                    Data = ReadData(open.FileName);
                    CleanData(Data);

                    fileBox.Text = Path.GetFileName(open.FileName);

                    dataTree.SelectedNode = dataTree.TopNode;

                    StageChanged?.Invoke(Stage.Validation);
                    FileChanged?.Invoke(open.FileName);

                    return true;
                }
                catch (IOException error)
                {
                    MessageBox.Show(error.Message);
                    return false;
                }                
            }
        }

        private async void TrackerTaskBegun()
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

                var importer = new ExcelImporter
                {
                    Query = Query,
                    Data = Data
                };

                tracker.SetSteps(importer);

                importer.NextItem += tracker.OnNextItem;
                importer.IncrementProgress += tracker.OnProgressChanged;
                importer.TaskFinished += FileImported;
                importer.TaskFailed += tracker.OnTaskFailed;

                await importer.Run();

                StageChanged.Invoke(Stage.Imported);
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                MessageBox.Show(error.Message);
            }
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

        private void OnFileButtonClicked(object sender, EventArgs e) => SelectFile();
    }
}
