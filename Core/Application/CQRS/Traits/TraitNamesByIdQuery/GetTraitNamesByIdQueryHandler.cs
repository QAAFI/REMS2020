using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries
{
    public class GetTraitByIdQueryHandler : IRequestHandler<GetTraitNamesByIdQuery, string[]>
    {
        private readonly IRemsDbFactory _factory;

        public GetTraitByIdQueryHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<string[]> Handle(GetTraitNamesByIdQuery request, CancellationToken cancellationToken)
        {
            if (_factory.Context == null) return null;

            var query = _factory.Context.Query(request.TraitIds);
            var ids = query.Cast<ITrait>().Select(t => t.TraitId).Distinct();

            var names = _factory.Context.Traits.Where(t => ids.Contains(t.TraitId)).Select(t => t.Name);
            return names.ToArray();
            //var traits = request.TraitIds.Select(id => _factory.Context.Traits.First(t => t.TraitId == id));

            //return traits.Select(t => t.Name).ToArray();
        }
    }

}
