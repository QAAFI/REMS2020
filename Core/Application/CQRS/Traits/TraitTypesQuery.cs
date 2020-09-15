using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class TraitTypesQuery : IRequest<string[]> 
    { }

    public class TraitTypesQueryHandler : IRequestHandler<TraitTypesQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public TraitTypesQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(TraitTypesQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private string[] Handler(TraitTypesQuery request, CancellationToken token)
        {
            return _context.Traits
                .Select(t => t.Type)
                .Where(n => n != null)
                .Distinct()
                .OrderBy(n => n)
                .ToArray();
        }
    }
}
