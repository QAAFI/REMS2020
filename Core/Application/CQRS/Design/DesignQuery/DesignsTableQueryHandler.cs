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
        private readonly IRemsDbContext _context;

        public DesignsTableQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<DataTable> Handle(DesignsTableQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request, token));            
        }

        private DataTable Handler(DesignsTableQuery request, CancellationToken token)
        {
            var table = new DataTable("Designs");

            var names = _context.Factors.Select(f => f.Name);
            var type = "".GetType();

            var columns = names.Select(n => new DataColumn(n, type)).ToArray();

            table.Columns.AddRange(columns);

            foreach (var id in request.TreatmentIds)
            {
                var row = table.NewRow();
                var designs = _context.Designs.Where(d => d.TreatmentId == id);

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
