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
        private readonly IRemsDbContext _context;

        public GetTraitByIdQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(GetTraitNamesByIdQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request, token));
        }

        private string[] Handler(GetTraitNamesByIdQuery request, CancellationToken token)
        {
            var query = _context.Query(request.TraitIds);

            var ids = query.Cast<ITrait>().Select(t => t.TraitId).Distinct();

            var names = _context.Traits
                .Where(t => ids.Contains(t.TraitId))
                .Select(t => t.Name);

            return names.ToArray();
        }
    }

}
