using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Infrastructure
{
    /// <summary>
    /// Manages the validation and import process of data from excel spreadsheets
    /// </summary>
    public class ExcelImporter : TaskRunner
    {
        /// <summary>
        /// The set of data tables to import
        /// </summary>
        public IEnumerable<DataTable> Data { get; set; }

        /// <inheritdoc/>
        public override int Items => Data.Count();
        /// <inheritdoc/>
        public override int Steps => Data.Sum(d => d.Rows.Count);        
 
        /// <summary>
        /// Sequentially insert each table into the database
        /// </summary>
        public async override Task Run()
        {            
            if (!FileManager.Connected)
                throw new Exception("No existing database connection");

            foreach (DataTable table in Data)
                await InsertTable(table);       
        }

        /// <summary>
        /// Adds the given data table to the context
        /// </summary>
        private async Task InsertTable(DataTable table)
        {
            // Skip any table that the importer is ignoring
            if (table.ExtendedProperties["Ignore"] is true)
                return;

            OnNextItem("Importing table");

            var type = table.ExtendedProperties["Type"] as Type;

            IRequest<Unit> command;

            //Change the insertion process depending on the table being added
            switch (table.TableName)
            {
                case "Design":
                    await Handler.Query(new InsertDesignsCommand { Table = table });
                    command = new InsertPlotsCommand
                    {
                        Table = table,
                        Progress = Reporter,
                        Confirmer = Handler
                    };
                    break;

                case "Experiments":
                    command = new InsertExperimentsTableCommand
                    {
                        Table = table,
                        Progress = Reporter
                    };
                    break;

                case "HarvestData":
                case "PlotData":
                    command = new InsertPlotDataTableCommand
                    {
                        Table = table,
                        Skip = 4,
                        Type = "Crop",
                        Progress = Reporter
                    };
                    break;

                case "MetData":
                    command = new InsertMetDataTableCommand
                    {
                        Table = table,
                        Skip = 2,
                        Type = "Climate",
                        Progress = Reporter
                    };
                    break;

                case "SoilLayerData":
                    command = new InsertSoilLayerTableCommand
                    {
                        Table = table,
                        Skip = 5,
                        Type = "SoilLayer",
                        Progress = Reporter
                    };
                    break;

                case "Irrigation":
                case "Fertilization":
                case "Tillage":
                    command = new InsertOperationsTableCommand
                    {
                        Confirmer = Handler,
                        Table = table,
                        Type = type,
                        Progress = Reporter
                    };
                    break;

                case "SoilData":
                    command = new InsertSoilDataTableCommand
                    {
                        Table = table,
                        Skip = 3,
                        Type = "Soil",
                        Progress = Reporter
                    };
                    break;

                case "Soils":
                    command = new InsertSoilTableCommand
                    {
                        Table = table,
                        Type = type,
                        Progress = Reporter
                    };
                    break;

                case "SoilLayer":
                case "SoilLayers":
                    command = new InsertSoilLayerTraitsCommand
                    {
                        Table = table,
                        Type = type,
                        Progress = Reporter
                    };
                    break;

                default:
                    command = new InsertTableCommand
                    { 
                        Table = table,
                        Type = type,
                        Progress = Reporter
                    };                    
                    break;
            }
            
            await Handler.Query(command);
        }
    }
}