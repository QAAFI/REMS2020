using System;
using System.Collections.Generic;
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
    public class ExpectedQuery : ContextQuery<DataColumn[]>
    {
        public DataTable Table { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ExpectedQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataColumn[] Run()
        {
            var type = Table.ExtendedProperties["Type"] as Type;
            var cols = Table.Columns.OfType<DataColumn>();

            var expected = type.GetProperties()
                .Where(p => p.GetCustomAttribute<Expected>() is not null);

            var result = new List<DataColumn>();

            foreach (var prop in expected)
            {
                var att = prop.GetCustomAttribute<Expected>();

                var col = cols.FirstOrDefault(c => att.Names.Contains(c.ColumnName));

                if (col is not null)
                    result.Add(col);
            }
            
            return result.ToArray();
        }
    }
}