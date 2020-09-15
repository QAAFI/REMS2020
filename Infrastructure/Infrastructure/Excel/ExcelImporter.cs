using ExcelDataReader;
using MediatR;
using Rems.Application;
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
    public class ExcelImporter : IProgressTracker
    {
        public event EventManager.NextProgressHandler NextProgress;
        public event EventHandler IncrementProgress;
        public event EventHandler StopProgress;
        
        public DataSet Data { get; private set; }

        public int Items { get { return Data.Tables.Count; } }

        private readonly IMediator _mediator;

        public ExcelImporter(IMediator mediator, string filepath)
        {
            _mediator = mediator;

            EventManager.ProgressIncremented += OnProgressIncremented;

            ReadData(filepath);
        }

        private void OnProgressIncremented(object sender, EventArgs e)
        {
            IncrementProgress?.Invoke(sender, e);
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

        public async void Run()
        {              
            foreach (DataTable table in Data.Tables) 
                await InsertTable(table);

            StopProgress?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Adds the given data table to the context
        /// </summary>
        private Task<Unit> InsertTable(DataTable table)
        {
            if (!_mediator.Send(new ConnectionExists()).Result)
                throw new Exception("No existing database connection");

            // Remove any duplicate rows from the table
            table.RemoveDuplicateRows();            

            var args = new StartProgressArgs()
            {
                Maximum = table.Rows.Count,
                Item = table.TableName
            };
            NextProgress?.Invoke(null, args);            

            // These IF statements are awful code, but my hand has been forced since the data is
            // coming from poorly designed excel templates. I decided devising a better solution 
            // is currently not worth the effort.

            if (table.TableName == "Notes" || table.Rows.Count == 0) 
                return Task.Run(() => Unit.Value);            

            if (table.TableName == "Design") 
            {
                // Note: InsertDesigns should preceed InsertPlots
                _mediator.Send(new InsertDesignsCommand() { Table = table }).Wait();
                return _mediator.Send(new InsertPlotsCommand() { Table = table });                
            }

            if (table.TableName == "PlotData")
            {
                var command = new InsertPlotDataTableCommand()
                { 
                    Table = table, 
                    Skip = 4,
                    Type = "Crop"
                };
                return _mediator.Send(command);
            }

            if (table.TableName == "MetData")
            {
                var command = new InsertMetDataTableCommand()
                {
                    Table = table,
                    Skip = 2,
                    Type = "Climate"
                };
                return _mediator.Send(command);
            }

            if (table.TableName == "SoilLayerData") 
            {
                var command = new InsertSoilLayerTableCommand()
                {
                    Table = table,
                    Skip = 5,
                    Type = "SoilLayer"
                };
                return _mediator.Send(command);
            }

            var type = _mediator.Send(new EntityTypeQuery() { Name = table.TableName }).Result;
            if (type == null)
                return Task.Run(() => Unit.Value);

            if (table.TableName == "Irrigation" || table.TableName == "Fertilization")
            {
                var command = new InsertOperationsTableCommand() { Table = table, Type = type };
                return _mediator.Send(command);
            }

            // TODO: Find a better catch for trait tables
            // This only filters true negatives, not false positives or false negatives
            else if (type.GetProperties().Count() < table.Columns.Count)
            {
                var query = new EntityTypeQuery() { Name = type.Name + "Trait" };
                var dependency = _mediator.Send(query).Result;
                
                var command = new InsertTraitTableCommand()
                {
                    Table = table,
                    Type = type,
                    Dependency = dependency
                };
                return _mediator.Send(command);
            }
            else
            {
                return _mediator.Send(new InsertTableCommand() { Table = table, Type = type });
            }
        }

    }
}