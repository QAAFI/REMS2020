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

            Data.Tables.Remove("Notes");            
            Data.FindExperiments();
            if (Format == "Data")
                Data.Tables.Remove("Experiments");

            var data = Data.Tables.OfType<DataTable>();
            var tables = new Dictionary<ExcelTable, ExcelColumn[]>();

            foreach (var a in anons)
            {
                var table = data.FirstOrDefault(t => a.Format.Names.Contains(t.TableName));

                table.ExtendedProperties["Type"] = a.Type;
                table.RemoveDuplicateRows();
                table.ConvertExperiments();
                table.RemoveEmptyColumns();                

                var excel = new ExcelTable { Data = table, Type = a.Type, Required = a.Format.Required };
                
                tables.Add(excel, GetColumns(table, a.Type));
            }

            return tables;
        }

        private ExcelColumn[] GetColumns(DataTable table, Type type)
        {   
            var cols = table?.Columns.OfType<DataColumn>();

            var expected = type.GetProperties()
                .Where(p => p.GetCustomAttribute<Expected>() is not null);

            var columns = new List<ExcelColumn>();

            foreach (var prop in expected)
            {
                var att = prop.GetCustomAttribute<Expected>();

                var col = cols?.FirstOrDefault(c => att.Names.Contains(c.ColumnName));
                if (col is not null)
                    col.ColumnName = prop.Name;

                var xl = new ExcelColumn
                {
                    Info = prop,
                    Data = col ?? new DataColumn(prop.Name + " not found"),
                };

                columns.Add(xl);
            }

            return columns.ToArray();
        }
    }
}