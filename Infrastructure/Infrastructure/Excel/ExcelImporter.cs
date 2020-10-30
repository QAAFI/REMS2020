using ExcelDataReader;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Infrastructure.Excel
{
    public class ExcelImporter : ProgressTracker
    {
        public DataSet Data { get; private set; }

        public override int Items { get; protected set; } = 0;
        public override int Steps { get; protected set; } = 0;

        private bool initialised = false;

        public ExcelImporter(QueryHandler query, CommandHandler command) : base(query, command)
        { }

        public bool Initialise(string filepath)
        {
            Data = ReadData(filepath);

            // Clean up tables and find metadata
            foreach (DataTable table in Data.Tables)
            {
                // Remove any duplicate rows from the table
                table.RemoveDuplicateRows();

                if (table.TableName == "Notes" || table.Rows.Count == 0) continue;

                Items++;
                Steps += table.Rows.Count;

                var type = OnSendQuery(new EntityTypeQuery() { Name = table.TableName });
                if (type == null) throw new Exception("Cannot import unrecognised table: " + table.TableName);

                table.ExtendedProperties.Add("Type", type);
            }

            // For each data table
            var invalids = Data.Tables.Cast<DataTable>()
                .Where(table => table.ExtendedProperties["Type"] != null)
                // From the data columns
                .Select(table => table.Columns.Cast<DataColumn>())
                // Where the column is not a property of an entity
                .SelectMany(cols => cols.Where(col => !IsProperty(col)))
                // Select the column name
                .Select(col => col.ColumnName)
                .ToArray();

            if (invalids.Any())
            {
                OnFoundInvalids(invalids);
                return false;
            }

            return initialised = true;
        }

        private bool IsProperty(DataColumn col)
        {
            var type = col.Table.ExtendedProperties["Type"] as Type;
            
            if (type.GetProperty(col.ColumnName) is PropertyInfo)
                return true;

            if (col.ColumnName == type.Name && type.GetProperty("Name") is PropertyInfo)
            {
                col.ColumnName = type.Name;
                return true;
            }

            var validater = EventManager.InvokeItemNotFound(col.ColumnName);
            if (validater.IsValid || validater.Ignore)
                return true;

            return false;
        }

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

        public async override Task Run()
        {
            if (!initialised) throw new Exception("The importer has not been initialised");

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
            // Skip the empty / notes tables
            if (table.TableName == "Notes" || table.Rows.Count < 1)
                return Task.Run(() => Unit.Value);

            OnNextItem(table.TableName);

            var type = table.ExtendedProperties["Type"] as Type;

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

                case "Soils":
                case "SoilLayer":
                    var query = new EntityTypeQuery() { Name = type.Name + "Trait" };
                    var dependency = OnSendQuery(query);
                    command = new InsertTraitTableCommand()
                    {
                        Table = table,
                        Type = type,
                        Dependency = dependency
                    };
                    break;

                default:
                    command = new InsertTableCommand() { Table = table, Type = type };                    
                    break;
            }
            return OnSendCommand(command);
        }

    }
}