using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Inserts design data into the database
    /// </summary>
    public class InsertDesignsCommand : IRequest
    {
        /// <summary>
        /// The table containing design data
        /// </summary>
        public DataTable Table { get; set; }
    }

    public class InsertDesignsCommandHandler : IRequestHandler<InsertDesignsCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertDesignsCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertDesignsCommand request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));

        private Unit Handler(InsertDesignsCommand request)
        {
            // Assume the first four columns contain other data
            var cols = request.Table.Columns.Cast<DataColumn>().Skip(4);
            var rows = request.Table.Rows.Cast<DataRow>();
            
            foreach (var c in cols)
            {
                // Find or create a factor for the column
                var factor = _context.Factors.FirstOrDefault(f => f.Name == c.ColumnName);
                if (factor is null)
                {
                    factor = new Factor() { Name = c.ColumnName };
                    _context.Attach(factor);
                }

                // For the factor column, convert each row into a level
                var levels = rows
                    .Select(r => r[c])
                    .Where(o => !(o is DBNull))
                    .Select(o => o.ToString())
                    .Distinct()
                    .ToArray();

                // Add the levels as entities
                foreach (string name in levels)
                {
                    var level = _context.Levels.Where(l => l.Name == name)
                        .Where(l => l.Factor == factor)
                        .FirstOrDefault();

                    if (level is null)
                    {
                        level = new Level() { Name = name, Factor = factor };
                        _context.Attach(level);
                    }
                }
            }

            _context.SaveChanges();

            return Unit.Value;
        }

    }
}
