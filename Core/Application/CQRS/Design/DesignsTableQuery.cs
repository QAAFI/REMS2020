using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Searches the database for information to populate an experiment design table
    /// </summary>
    public class DesignsTableQuery : IRequest<DataTable>
    {
        /// <summary>
        /// The experiment to find design data for
        /// </summary>
        public int ExperimentId { get; set; }
    }

    public class DesignsTableQueryHandler : IRequestHandler<DesignsTableQuery, DataTable>
    {
        private readonly IRemsDbContext _context;

        public DesignsTableQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<DataTable> Handle(DesignsTableQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private DataTable Handler(DesignsTableQuery request, CancellationToken token)
        {
            var exp = _context.Experiments.Find(request.ExperimentId);

            var table = new DataTable("Designs");

            var names = _context.Factors.Select(f => f.Name).Distinct();
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
