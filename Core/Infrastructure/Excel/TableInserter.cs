﻿using MediatR;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Rems.Infrastructure.Excel
{
    /// <summary>
    /// Manages the validation and import process of data from excel spreadsheets
    /// </summary>
    public class TableInserter : ITaskRunner
    {
        /// <inheritdoc/>
        public event EventHandler<string> NextItem;

        /// <inheritdoc/>
        public IQueryHandler Handler { get; set; }

        /// <inheritdoc/>
        public IProgress<int> Progress { get; set; }

        /// <summary>
        /// The set of data tables to import
        /// </summary>
        public IEnumerable<DataTable> Data { get; set; }

        /// <inheritdoc/>
        public int Items => Data.Count();
        /// <inheritdoc/>
        public int Steps => Data.Sum(d => d.Rows.Count);        
 
        /// <summary>
        /// Sequentially insert each table into the database
        /// </summary>
        public async Task Run()
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

            NextItem.Invoke(this, "Importing table");

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
                        Progress = Progress,
                        Confirmer = Handler
                    };
                    break;

                case "Experiments":
                    command = new InsertExperimentsTableCommand
                    {
                        Table = table,
                        Progress = Progress
                    };
                    break;

                case "HarvestData":
                case "PlotData":
                    command = new InsertPlotDataTableCommand
                    {
                        Table = table,
                        Skip = 4,
                        Type = "Crop",
                        Progress = Progress
                    };
                    break;

                case "MetData":
                    command = new InsertMetDataTableCommand
                    {
                        Table = table,
                        Skip = 2,
                        Type = "Climate",
                        Progress = Progress
                    };
                    break;

                case "SoilLayerData":
                    command = new InsertSoilLayerTableCommand
                    {
                        Table = table,
                        Skip = 5,
                        Type = "SoilLayer",
                        Progress = Progress
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
                        Progress = Progress
                    };
                    break;

                case "SoilData":
                    command = new InsertSoilDataTableCommand
                    {
                        Table = table,
                        Skip = 3,
                        Type = "Soil",
                        Progress = Progress
                    };
                    break;

                case "Soils":
                    command = new InsertSoilTableCommand
                    {
                        Table = table,
                        Type = type,
                        Progress = Progress
                    };
                    break;

                case "SoilLayer":
                case "SoilLayers":
                    command = new InsertSoilLayerTraitsCommand
                    {
                        Table = table,
                        Type = type,
                        Progress = Progress
                    };
                    break;

                default:
                    command = new InsertTableCommand
                    { 
                        Table = table,
                        Type = type,
                        Progress = Progress
                    };                    
                    break;
            }
            
            await Handler.Query(command);
        }
    }
}