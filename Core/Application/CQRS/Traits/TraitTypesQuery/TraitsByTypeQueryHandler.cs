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
        private readonly IRemsDbFactory factory;

        public TraitTypesQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<string[]> Handle(TraitTypesQuery request, CancellationToken cancellationToken)
        {            
            return factory.Context.Traits
                .Select(t => t.Type)
                .Distinct()
                .OrderBy(n => n)
                .ToArray();
        }
    }

}
