using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class TraitExistsQuery : IRequest 
    {
        public IItemValidater Validater { get; set; }
    }

    public class TraitExistsQueryHandler : IRequestHandler<TraitExistsQuery>
    {
        private readonly IRemsDbContext _context;

        public TraitExistsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(TraitExistsQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private Unit Handler(TraitExistsQuery request, CancellationToken token)
        {
            // TODO: Search could be more efficient. Just getting it working.

            bool valid = false || _context.Traits.Any(t => t.Name == request.Validater.Name);

            foreach (var item in request.Validater.Values.Split(','))
                valid = valid || _context.Traits.Any(t => t.Name == item);

            request.Validater.IsValid = valid;

            return Unit.Value;
        }
    }
}
