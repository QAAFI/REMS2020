using ExcelDataReader;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Rems.Infrastructure.Excel;

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
        /// Occurs when data is requested from the mediator
        /// </summary>
        public event Func<object, Task<object>> Query;

        /// <summary>
        /// Occurs after a file is imported
        /// </summary>
        public event Action FileImported;

        /// <summary>
        /// Occurs when the current import stage has changed
        /// </summary>
        public event Action<Stage> StageChanged;

        /// <summary>
        /// Occurs when the file to import from has changed
        /// </summary>
        public event Action<string> FileChanged;

        private async Task<T> InvokeQuery<T>(IRequest<T> query)
            => (T)await Query(query);

        /// <summary>
        /// The system folder most recently accessed by the user
        /// </summary>
        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// Collection of icons used by the data tree
        /// </summary>
        private ImageList images;

        public Importer() : base()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            // Add icons to the image list
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
            dataTree.AfterLabelEdit += AfterLabelEdit;

            tracker.TaskBegun += RunImporter;
        }

        private void AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node is DataNode node && e.Label != null)
            {
                node.Excel.Name = e.Label;
                node.Validate();
            }
        }

        #region Data

        /// <summary>
        /// The excel data
        /// </summary>
        public DataSet Data { get; set; } 

        /// <summary>
        /// Reads the data from the given file
        /// </summary>
        /// <remarks>
        /// It is assumed that the file is either of .xls or .xlsx format
        /// </remarks>
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

        /// <summary>
        /// Attempts to sanitise raw excel data so it can be read into the database
        /// </summary>        
        private async Task CleanData(DataSet data)
        {
            dataTree.Nodes.Clear();

            foreach (var table in data.Tables.Cast<DataTable>().ToArray())
            {   
                if (table.TableName == "Notes" || table.Rows.Count == 0)
                {
                    data.Tables.Remove(table);
                    continue;
                }

                await CleanTable(table);                                
            }

            // Separate loop to avoid ordering problems from async code
            foreach (var table in data.Tables.Cast<DataTable>().ToArray())
                dataTree.Nodes.Add(CreateTableNode(table));
        }

        public async Task CleanTable(DataTable table)
        {
            // TODO: This is a quick workaround, find better way to handle factors/levels table
            if (table.TableName == "Factors") table.TableName = "Levels";

            // TODO: This is a quick workaround, find better way to handle planting/sowing table
            if (table.TableName == "Planting") table.TableName = "Sowing";

            // Remove any duplicate rows from the table
            table.RemoveDuplicateRows();

            var type = await InvokeQuery(new EntityTypeQuery() { Name = table.TableName });
            if (type == null) throw new Exception("Cannot import unrecognised table: " + table.TableName);

            table.ExtendedProperties.Add("Type", type);

            // Remove empty columns
            var cols = table.Columns.Cast<DataColumn>().ToArray();
            foreach (var col in cols)
                if (col.ColumnName.Contains("Column"))
                    table.Columns.Remove(col);
        }

        /// <summary>
        /// Validate the contents of a table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private TreeNode CreateTableNode(DataTable table)
        {
            var xt = new ExcelTable(table);
            xt.Query += (o) => Query?.Invoke(o);

            var vt = CreateTableValidater(table);
            vt.SetAdvice += a => { adviceBox.Clear(); adviceBox.AddText(a); };

            var tnode = new DataNode(xt, vt);
            tnode.Query += (o) => Query?.Invoke(o);

            // Prepare individual columns for import
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var col = table.Columns[i];

                // Create a node for the column
                var xc = new ExcelColumn(col);

                INodeValidater vc;
                if (vt is CustomTableValidater ctv)
                {
                    if (i < ctv.columns.Length)
                        vc = new OrdinalValidater(col, i, ctv.columns[i]);
                    else
                        vc = new NullValidater();
                }                    
                else
                {
                    var validater = new ColumnValidater(col);
                    validater.Query += (o) => Query?.Invoke(o);
                    vc = validater;
                }
                vc.SetAdvice += a => { adviceBox.Clear();  adviceBox.AddText(a); };

                var cnode = new DataNode(xc, vc);
                cnode.Query += (o) => Query?.Invoke(o);
                cnode.Updated += () => { importData.DataSource = xc.Source; importData.Format(); };

                vc.Validate();

                tnode.Nodes.Add(cnode);
            }
            
            vt.Validate();

            return tnode;
        }

        public static INodeValidater CreateTableValidater(DataTable table)
        {
            string[] cols;
            switch (table.TableName)
            {
                case "Design":
                    cols = new string[] { "ExperimentId", "Treatment", "Repetition", "Plot" };
                    return new CustomTableValidater(table, cols);

                case "HarvestData":
                case "PlotData":
                    cols = new string[] { "ExperimentId", "Plot", "Date", "Sample" };
                    return new CustomTableValidater(table, cols);

                case "MetData":
                    cols = new string[] { "MetStation", "Date" };
                    return new CustomTableValidater(table, cols);

                case "SoilLayerData":
                    cols = new string[] { "ExperimentId", "Plot", "Date", "DepthFrom", "DepthTo" };
                    return new CustomTableValidater(table, cols);

                case "Irrigation":
                case "Fertilization":
                case "Tillage":
                    cols = new string[] { "ExperimentId", "Treatment" };
                    return new CustomTableValidater(table, cols);

                case "Soils":
                case "SoilLayer":
                case "SoilLayers":
                    return new TableValidater(table);

                default:
                    return new TableValidater(table);
            }
        }        

        #endregion
        /// <summary>
        /// Handles the selection of a new node in the tree
        /// </summary>
        private void TreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is DataNode node)
            {
                importData.DataSource = node.Excel.Source;                
                importData.Format();                

                columnLabel.Text = node.Text;

                adviceBox.Clear();
                if (node.Advice.Any())
                    adviceBox.AddText(node.Advice);                
                else if (node.Parent is DataNode parent)
                    adviceBox.AddText(parent.Advice);
            }
        }

        /// <summary>
        /// Lets the user select a file to open for import
        /// </summary>
        /// <returns>True if the file is valid, false otherwise</returns>
        public async Task<bool> OpenFile()
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
                    await CleanData(Data);

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

        /// <summary>
        /// Runs the excel importer
        /// </summary>
        private async void RunImporter()
        {
            try
            {
                bool connected = await InvokeQuery(new ConnectionExists());
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

                var importer = new ExcelImporter { Data = Data };
                importer.Query += (o) => Query?.Invoke(o);

                tracker.SetSteps(importer);

                importer.NextItem += tracker.OnNextTask;
                importer.IncrementProgress += tracker.OnProgressChanged;
                importer.TaskFinished += FileImported;
                importer.TaskFailed += tracker.OnTaskFailed;

                await importer.Run();

                tracker.Reset();

                StageChanged.Invoke(Stage.Imported);
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                MessageBox.Show(error.Message);
            }
        }        

        /// <summary>
        /// Handles the file button click event
        /// </summary>
        private async void OnFileButtonClicked(object sender, EventArgs e) => await OpenFile();
    }
}
