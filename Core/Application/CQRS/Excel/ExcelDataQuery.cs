using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Attributes;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Return a collection of all experiments paired by ID and Name
    /// </summary>
    public class ExcelDataQuery : ContextQuery<Dictionary<ExcelTable, ExcelColumn[]>>
    {
        public DataSet Data { get; set; }

        public string Format { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ExcelDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Dictionary<ExcelTable, ExcelColumn[]> Run()
        {
            var assembly = Assembly.Load("Rems.Domain");

            var anons = assembly.GetTypes()
                .Select(t => new {Type = t, Format = t.GetCustomAttribute<ExcelFormat>()})
                .Where(a => a.Format?.Format == Format)
                .OrderBy(a => a.Format.Dependency);

            if (Data.Tables.OfType<DataTable>().Any(t => t.TableName == "Notes"))
                Data.Tables.Remove("Notes");
            
            Data.FindExperiments();
            if (Format == "Data")
                Data.Tables.Remove("Experiments");

            var data = Data.Tables.OfType<DataTable>();
            var tables = new Dictionary<ExcelTable, ExcelColumn[]>();

            foreach (var a in anons)
            {
                var table = data.FirstOrDefault(t => a.Format.Names.Contains(t.TableName));

                if (table is not null)
                {
                    table.ExtendedProperties["Type"] = a.Type;
                    table.RemoveDuplicateRows();                    
                    table.RemoveEmptyColumns();

                    if (a.Type.GetProperty("Experiment") is PropertyInfo info)
                        table.ConvertExperiments(info.GetCustomAttribute<Expected>().Names);
                }

                var excel = new ExcelTable { Data = table, Type = a.Type, Required = a.Format.Required };
                tables.Add(excel, excel.GetColumns(a.Type));
            }

            return tables;
        }

        
    }
}