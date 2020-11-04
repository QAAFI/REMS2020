using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System.IO;
using ExcelDataReader;
using Rems.Infrastructure.Excel;
using Rems.Application.Common;
using WindowsClient.Forms;

namespace WindowsClient.Controls
{
    public partial class Importer : UserControl
    {
        public QueryHandler Query;

        public CommandHandler Command;

        public event Action DatabaseChanged;

        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private ExcelImporter importer;

        public Importer()
        {
            InitializeComponent();            
        }

        public void Initialise()
        {
            importer = new ExcelImporter(Query, Command);
            importer.ItemNotFound += importValidater.HandleMissingItem;

            importValidater.SendQuery = Query;
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
            foreach (var table in data.Tables.Cast<DataTable>().ToArray())
            {
                if (table.TableName == "Notes" || table.Rows.Count == 0)
                {
                    data.Tables.Remove(table);
                    continue;
                }

                // Remove any duplicate rows from the table
                table.RemoveDuplicateRows();
                
                var type = Query(new EntityTypeQuery() { Name = table.TableName }).Result;
                if (type == null) throw new Exception("Cannot import unrecognised table: " + table.TableName);

                table.ExtendedProperties.Add("Type", type);
                
                tableBox.Items.Add(table.TableName);
            }

            tableBox.Refresh();
        }

        private bool ValidateData(DataSet data)
        {
            // For each data table
            var invalids = data.Tables.Cast<DataTable>()
                .Where(table => table.ExtendedProperties["Type"] != null)
                // From the data columns
                .Select(table => table.Columns.Cast<DataColumn>())
                // Where the column is not a property of an entity
                .SelectMany(cols => cols.Where(col => col.FindProperty(importValidater.HandleMissingItem) == null))
                .ToArray();

            if (invalids.Any())
            {
                importValidater.OnFoundInvalids(invalids);
                return false;
            }

            return true;
        }

        #endregion

        private void OnTableSelected(object sender, EventArgs e)
        {
            var name = (string)tableBox.SelectedItem;
            importData.DataSource = importer.Data.Tables[name];
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
                    ValidateData(importer.Data);
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

                if (!importValidater.AllValid())
                {
                    MessageBox.Show("Invalid import options found in validater");
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
    }
}
