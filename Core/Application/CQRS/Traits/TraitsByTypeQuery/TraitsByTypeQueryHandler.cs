using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries
{
    public class TraitsByTypeQueryHandler : IRequestHandler<TraitsByTypeQuery, string[]>
    {
        private readonly IRemsDbFactory factory;

        public TraitsByTypeQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<string[]> Handle(TraitsByTypeQuery request, CancellationToken cancellationToken)
        {            
            return factory.Context.Traits.Where(t => t.Type == request.Type).Select(t => t.Name).ToArray();
        }
    }

}
