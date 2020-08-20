using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries
{
    public class TraitTypesQueryHandler : IRequestHandler<TraitTypesQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public TraitTypesQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(TraitTypesQuery request, CancellationToken token)
        {
            return Task.Run(() => _context.Traits
                .Select(t => t.Type)
                .Distinct()
                .OrderBy(n => n)
                .ToArray()
            );
        }
    }

}
