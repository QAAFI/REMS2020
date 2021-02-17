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
    public class ExcelImporter : ProgressTracker
    {
        public DataSet Data { get; set; }

        public override int Items => Data.Tables.Count;
        public override int Steps => Data.Tables.Cast<DataTable>().Sum(d => d.Rows.Count);        
 
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
            // Skip the empty / notes tables
            if (table.ExtendedProperties["Ignore"] is true)
                return Unit.Value;

            OnNextItem(table.TableName);

            var type = table.ExtendedProperties["Type"] as Type;

            IRequest command;
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
                case "SoilLayer":
                case "SoilLayers":
                    var query = new EntityTypeQuery() 
                    { 
                        Name = type.Name + "Trait"
                    };
                    var dependency = await InvokeQuery(query);

                    command = new InsertTraitTableCommand()
                    {
                        Table = table,
                        Type = type,
                        Dependency = dependency,
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