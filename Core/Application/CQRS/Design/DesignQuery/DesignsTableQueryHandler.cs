using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Domain.Entities;
using Rems.Application.Common.Interfaces;

using System.Data;
using System.Linq;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class DesignsTableQueryHandler : IRequestHandler<DesignsTableQuery, DataTable>
    {
        private readonly IRemsDbFactory factory;

        public DesignsTableQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<DataTable> Handle(DesignsTableQuery request, CancellationToken token)
        {
            var table = new DataTable("Designs");

            var names = factory.Context.Factors.Select(f => f.Name);
            var type = "".GetType();

            var columns = names.Select(n => new DataColumn(n, type)).ToArray();           

            table.Columns.AddRange(columns);

            foreach (var id in request.TreatmentIds)
            {
                var row = table.NewRow();
                var designs = factory.Context.Designs.Where(d => d.TreatmentId == id);

                foreach (var design in designs)
                {
                    row[design.Level.Factor.Name] = design.Level.Name;
                }

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
