using ExcelDataReader;
using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Infrastructure.Excel
{
    public class ExcelImporter : ProgressTracker
    {
        public DataSet Data { get; private set; }

        public override int Items { get { return Data.Tables.Count; } }

        public ExcelImporter(string filepath) : base()
        {         
            ReadData(filepath);
        }

        private void OnProgressIncremented(object sender, EventArgs e)
        {
            OnIncrementProgress();
        }

        private void ReadData(string filepath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    Data = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                }
            }
        }

        public async override Task Run()
        {
            try
            {
                if (!OnSendQuery(new ConnectionExists()))
                    throw new Exception("No existing database connection");

                foreach (DataTable table in Data.Tables)
                    await InsertTable(table);

                OnTaskFinished();
            }
            catch (Exception e)
            {
                OnTaskFailed(e);
            }      
        }

        /// <summary>
        /// Adds the given data table to the context
        /// </summary>
        private Task InsertTable(DataTable table)
        {            
            // Remove any duplicate rows from the table
            table.RemoveDuplicateRows();            

            var args = new NextItemArgs()
            {
                Maximum = table.Rows.Count,
                Item = table.TableName
            };
            OnNextItem(null, args);

            // Skip the empty / notes tables
            if (table.TableName == "Notes" || table.Rows.Count < 1)
                return Task.Run(() => Unit.Value);

            var type = OnSendQuery(new EntityTypeQuery() { Name = table.TableName });
            if (type == null) throw new Exception("Cannot import unrecognised table: " + table.TableName);

            IRequest command;
            switch (table.TableName)
            {
                case "Design":
                    OnSendCommand(new InsertDesignsCommand() { Table = table }).Wait();
                    command = new InsertPlotsCommand() { Table = table };
                    break;

                case "PlotData":
                    command = new InsertPlotDataTableCommand()
                    {
                        Table = table,
                        Skip = 4,
                        Type = "Crop"
                    };
                    break;

                case "MetData":
                    command = new InsertMetDataTableCommand()
                    {
                        Table = table,
                        Skip = 2,
                        Type = "Climate"
                    };
                    break;

                case "SoilLayerData":
                    command = new InsertSoilLayerTableCommand()
                    {
                        Table = table,
                        Skip = 5,
                        Type = "SoilLayer"
                    };
                    break;

                case "Irrigation":
                case "Fertilization":
                    command = new InsertOperationsTableCommand() { Table = table, Type = type };
                    break;

                default:
                    // TODO: Find a better catch for trait tables
                    // This only filters true negatives, not false positives or false negatives
                    if (type.GetProperties().Count() < table.Columns.Count)
                    {
                        var query = new EntityTypeQuery() { Name = type.Name + "Trait" };
                        var dependency = OnSendQuery(query);
                        command = new InsertTraitTableCommand()
                        {
                            Table = table,
                            Type = type,
                            Dependency = dependency
                        };
                    }
                    else
                    {
                        command = new InsertTableCommand() { Table = table, Type = type };
                    }
                    break;
            }
            return OnSendCommand(command);
        }

    }
}