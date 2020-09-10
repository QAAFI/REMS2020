using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class TraitsByTypeQuery : IRequest<string[]> 
    {
        public string Type { get; set; }
    }

    public class TraitsByTypeQueryHandler : IRequestHandler<TraitsByTypeQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public TraitsByTypeQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(TraitsByTypeQuery request, CancellationToken token)
        {
            return Task.Run(() => _context.Traits
                .Where(t => t.Type == request.Type)
                .Select(t => t.Name)
                .OrderBy(n => n)
                .ToArray()
            );
        }
    }
}
