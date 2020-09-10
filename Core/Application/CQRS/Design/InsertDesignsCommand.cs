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
    public class InsertDesignsCommand : IRequest<Unit>
    {
        public DataTable Table { get; set; }
    }

    public class InsertDesignsCommandHandler : IRequestHandler<InsertDesignsCommand, Unit>
    {
        private readonly IRemsDbContext _context;

        public InsertDesignsCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(InsertDesignsCommand request, CancellationToken cancellationToken)
        {
            var rows = request.Table.Rows.Cast<DataRow>();

            foreach (var c in request.Table.Columns.Cast<DataColumn>().Skip(4))
            {
                var factor = _context.Factors.FirstOrDefault(f => f.Name == c.ColumnName);
                if (factor is null)
                {
                    factor = new Factor() { Name = c.ColumnName };
                    _context.Attach(factor);
                }

                var levels = rows
                    .Select(r => r[c])
                    .Where(o => !(o is DBNull))
                    .Select(o => o.ToString())
                    .Distinct()
                    .ToArray();

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
