using ExcelDataReader;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Application;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Infrastructure.Excel
{
    public class ExcelImporter
    {
        private readonly IMediator _mediator;

        public ExcelImporter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public DataSet ReadDataSet(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
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
        
        public void InsertDataSet(DataSet data)
        {
            var args = new ProgressTrackingArgs()
            {
                Items = data.Tables.Count,
                Title = "Importing..."
            };

            EventManager.InvokeProgressTracking(null, args);

            foreach (DataTable table in data.Tables) InsertTable(table);

            EventManager.InvokeStopProgress(null, EventArgs.Empty);
        }

        /// <summary>
        /// Adds the given data table to the context
        /// </summary>
        private void InsertTable(DataTable table)
        {
            // Remove any duplicate rows from the table
            var rows = table.DistinctRows().Select(r => r.ItemArray).ToArray();
            table.Rows.Clear();
            foreach (var row in rows) table.Rows.Add(row);

            var args = new StartProgressArgs()
            {
                Maximum = rows.Count(),
                Item = table.TableName
            };
            EventManager.InvokeStartProgress(null, args);

            if (table.TableName == "Notes") return;
            if (table.Rows.Count == 0) return;

            // These IF statements are awful code, but my hand has been forced since the data is
            // coming from poorly designed excel templates. I decided devising a better solution 
            // is currently not worth the effort.

            if (table.TableName == "Design") 
            {
                // Note: InsertDesigns should preceed InsertPlots
                _mediator.Send(new InsertDesignsCommand() { Table = table });
                _mediator.Send(new InsertPlotsCommand() { Table = table });
                return;
            }

            if (table.TableName == "PlotData")
            {
                var command = new InsertPlotDataTableCommand()
                { 
                    Table = table, 
                    Skip = 4,
                    Type = "Crop"
                };
                _mediator.Send(command).Wait();
                return;
            }

            if (table.TableName == "MetData")
            {
                var command = new InsertMetDataTableCommand()
                {
                    Table = table,
                    Skip = 2,
                    Type = "Climate"
                };
                _mediator.Send(command).Wait();
                return;
            }

            if (table.TableName == "SoilLayerData") 
            {
                var command = new InsertSoilLayerTableCommand()
                {
                    Table = table,
                    Skip = 5,
                    Type = "SoilLayer"
                };
                _mediator.Send(command).Wait();
                return;
            }

            var type = _mediator.Send(new EntityTypesQuery() { Name = table.TableName }).Result;
            if (type == null) return;

            if (table.TableName == "Irrigation" || table.TableName == "Fertilization")
            { 
                _mediator.Send(new InsertOperationsTableCommand() { Table = table, Type = type });
            }

            // TODO: Find a better catch for trait tables
            // This only filters true negatives, not false positives or false negatives
            else if (type.GetProperties().Count() < table.Columns.Count)
            {
                var dependency = _mediator.Send(new EntityTypesQuery() { Name = type.Name + "Trait" }).Result;
                var command = new InsertTraitTableCommand()
                {
                    Table = table,
                    Type = type,
                    Dependency = dependency
                };
                _mediator.Send(command);
            }
            else
            {
                _mediator.Send(new InsertTableCommand() { Table = table, Type = type });
            }
        }
    }
}