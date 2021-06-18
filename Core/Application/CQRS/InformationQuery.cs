using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Attributes;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Return a collection of all experiments paired by ID and Name
    /// </summary>
    public class InformationQuery : ContextQuery<Dictionary<ExcelTable, ExcelColumn[]>>
    {
        public DataSet Data { get; set; }

        public string Format { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InformationQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Dictionary<ExcelTable, ExcelColumn[]> Run()
        {
            var assembly = Assembly.Load("Rems.Domain");

            var types = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<ExcelFormat>()?.Format == Format);

            var data = Data.Tables.OfType<DataTable>();
            var tables = new Dictionary<ExcelTable, ExcelColumn[]>();

            foreach (var type in types)
            {
                var names = type.GetCustomAttribute<ExcelFormat>().Names;
                var table = data.FirstOrDefault(t => names.Contains(t.TableName));

                var excel = new ExcelTable { Data = table, Type = type };

                if (excel.Valid = table is not null)
                    tables.Add(excel, GetColumns(table, type));
            }

            return tables;
        }

        private ExcelColumn[] GetColumns(DataTable table, Type type)
        {
            var cols = table.Columns.OfType<DataColumn>();

            var expected = type.GetProperties()
                .Where(p => p.GetCustomAttribute<Expected>() is not null);

            var columns = new List<ExcelColumn>();

            foreach (var prop in expected)
            {
                var att = prop.GetCustomAttribute<Expected>();

                var col = cols.FirstOrDefault(c => att.Names.Contains(c.ColumnName));
                if (col is not null)
                    col.ColumnName = prop.Name;

                var xl = new ExcelColumn
                {
                    Info = prop,
                    Data = col ?? new DataColumn(prop.Name + " not found"),
                    Valid = col is not null
                };

                columns.Add(xl);
            }

            return columns.ToArray();
        }
    }
}