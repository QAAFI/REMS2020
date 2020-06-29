using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Attributes;
using Rems.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries
{
    public class GetGraphableItemsQueryHandler : IRequestHandler<GetGraphableItemsQuery, string[]>
    {
        private readonly IRemsDbFactory _factory;
        public GetGraphableItemsQueryHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<string[]> Handle(GetGraphableItemsQuery request, CancellationToken cancellationToken)
        {
            if (_factory.Context == null) return null;

            var query = _factory.Context.Query(request.TableName);
            var items = query.Cast<IEntity>().FirstOrDefault().GetPropertyByAttribute<Graphable>();

            return items.Select(p => p.Name).ToArray();
        }
    }

}
