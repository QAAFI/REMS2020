using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Attributes;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Return a collection of all experiments paired by ID and Name
    /// </summary>
    public class InformationQuery : ContextQuery<DataSet>
    {
        public DataSet Data { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InformationQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataSet Run()
        {
            var assembly = Assembly.Load("Rems.Domain");

            var types = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<ExcelSource>()?.Source == RemsSource.Information);

            var remsData = new DataSet("RemsData");
            var tables = Data.Tables.OfType<DataTable>();

            foreach (var type in types)
            {
                var names = type.GetCustomAttribute<ExcelSource>().Names;
                var table = tables.FirstOrDefault(t => names.Contains(t.TableName));
                
                if (table is not null)
                {
                    table.ExtendedProperties["Type"] = type;
                    Data.Tables.Remove(table);
                    remsData.Tables.Add(table);                    
                }
                else
                {
                    var missing = new DataTable(names[0] + " not found");
                    missing.ExtendedProperties["Type"] = type;
                    remsData.Tables.Add(missing);
                }
            }

            return remsData;
        }
    }
}