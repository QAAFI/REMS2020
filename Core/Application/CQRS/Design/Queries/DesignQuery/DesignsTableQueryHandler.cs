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
            var exp = _context.Experiments.Find(request.ExperimentId);

            var table = new DataTable("Designs");

            var names = _context.Factors.Select(f => f.Name);
            var type = "".GetType();

            var columns = names.Select(n => new DataColumn(n, type)).ToArray();

            table.Columns.AddRange(columns);

            foreach (var treatment in exp.Treatments)
            {
                var row = table.NewRow();

                foreach (var design in treatment.Designs) row[design.Level.Factor.Name] = design.Level.Name;
                
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
