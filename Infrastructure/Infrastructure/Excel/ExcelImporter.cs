using MediatR;
using Rems.Application.Common;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Infrastructure.Excel
{
    /// <summary>
    /// Manages the validation and import process of data from excel spreadsheets
    /// </summary>
    public class ExcelImporter : ProgressTracker
    {
        /// <summary>
        /// The set of data tables to import
        /// </summary>
        public DataSet Data { get; set; }

        /// <inheritdoc/>
        public override int Items => Data.Tables.Count;
        /// <inheritdoc/>
        public override int Steps => Data.Tables.Cast<DataTable>().Sum(d => d.Rows.Count);        
 
        /// <summary>
        /// Sequentially insert each table into the database
        /// </summary>
        public async override Task Run()
        {
            try
            {
                if (! await InvokeQuery(new ConnectionExists()))
                    throw new Exception("No existing database connection");

                foreach (DataTable table in Data.Tables)
                    await InsertTable(table);

                Thread.Sleep(500);

                OnTaskFinished();
            }
            catch (InvalidOperationException e)
            {
                OnTaskFailed(new Exception("Import failed. Data contains duplicate measurements."));
            }
            catch (Exception e)
            {
                OnTaskFailed(e);
            }      
        }

        /// <summary>
        /// Adds the given data table to the context
        /// </summary>
        private async Task<Unit> InsertTable(DataTable table)
        {
            // Skip any table that the importer is ignoring
            if (table.ExtendedProperties["Ignore"] is true)
                return Unit.Value;

            OnNextItem(table.TableName);

            var type = table.ExtendedProperties["Type"] as Type;

            IRequest command;

            //Change the insertion process depending on the table being added
            switch (table.TableName)
            {
                case "Design":
                    await InvokeQuery(new InsertDesignsCommand() { Table = table });
                    command = new InsertPlotsCommand() 
                    { 
                        Table = table,
                        IncrementProgress = OnIncrementProgress
                    };
                    break;

                case "HarvestData":
                case "PlotData":
                    command = new InsertPlotDataTableCommand()
                    {
                        Table = table,
                        Skip = 4,
                        Type = "Crop",
                        IncrementProgress = OnIncrementProgress
                    };
                    break;

                case "MetData":
                    command = new InsertMetDataTableCommand()
                    {
                        Table = table,
                        Skip = 2,
                        Type = "Climate",
                        IncrementProgress = OnIncrementProgress
                    };
                    break;

                case "SoilLayerData":
                    command = new InsertSoilLayerTableCommand()
                    {
                        Table = table,
                        Skip = 5,
                        Type = "SoilLayer",
                        IncrementProgress = OnIncrementProgress
                    };
                    break;

                case "Irrigation":
                case "Fertilization":
                case "Tillage":
                    command = new InsertOperationsTableCommand() 
                    { 
                        Table = table, 
                        Type = type,
                        IncrementProgress = OnIncrementProgress
                    };
                    break;

                case "Soils":
                    command = new InsertSoilTableCommand
                    {
                        Table = table,
                        Type = type,
                        IncrementProgress = OnIncrementProgress
                    };
                    break;

                case "SoilLayer":
                case "SoilLayers":
                    command = new InsertSoilLayerTraitsCommand()
                    {
                        Table = table,
                        Type = type,
                        IncrementProgress = OnIncrementProgress
                    };
                    break;

                default:
                    command = new InsertTableCommand()
                    { 
                        Table = table,
                        Type = type,
                        IncrementProgress = OnIncrementProgress
                    };                    
                    break;
            }
            return await InvokeQuery(command);
        }

    }
}