using MediatR;
using Rems.Application.Common;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Rems.Infrastructure.Excel
{
    public class ExcelImporter : ProgressTracker
    {
        public DataSet Data { get; set; }

        public override int Items => Data.Tables.Count;
        public override int Steps => Data.Tables.Cast<DataTable>().Sum(d => d.Rows.Count);

        public ExcelImporter(QueryHandler query) : base(query)
        { }        

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
            // Skip the empty / notes tables
            if (table.ExtendedProperties["Ignored"] is true)
                return Task.Run(() => Unit.Value);

            OnNextItem(table.TableName);

            var type = table.ExtendedProperties["Type"] as Type;

            IRequest command;
            switch (table.TableName)
            {
                case "Design":
                    OnSendQuery(new InsertDesignsCommand() { Table = table });
                    command = new InsertPlotsCommand() { Table = table };
                    break;

                case "HarvestData":
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
                    command = new InsertOperationsTableCommand() 
                    { 
                        Table = table, 
                        Type = type
                    };
                    break;

                case "Soils":
                case "SoilLayer":
                case "SoilLayers":
                    var query = new EntityTypeQuery() 
                    { 
                        Name = type.Name + "Trait"
                    };
                    var dependency = OnSendQuery(query);

                    command = new InsertTraitTableCommand()
                    {
                        Table = table,
                        Type = type,
                        Dependency = dependency
                    };
                    break;

                default:
                    command = new InsertTableCommand()
                    { 
                        Table = table,
                        Type = type
                    };                    
                    break;
            }
            return Task.Run(() => OnSendQuery(command));
        }

    }
}