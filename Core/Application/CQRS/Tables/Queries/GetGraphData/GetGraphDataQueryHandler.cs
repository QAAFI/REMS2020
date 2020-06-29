using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Attributes;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries
{
    public class GetGraphDataQueryHandler : IRequestHandler<GetGraphDataQuery, IQueryable<Tuple<object, object>>>
    {
        private readonly IRemsDbFactory _factory;
        public GetGraphDataQueryHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<IQueryable<Tuple<object, object>>> Handle(GetGraphDataQuery request, CancellationToken cancellationToken)
        {
            if (_factory.Context == null) return null;

            var query = _factory.Context.Query(request.TableName).Cast<ITrait>();
            var trait = _factory.Context.Traits.First(t => t.Name == request.TraitName);

            var x = query.FirstOrDefault().GetPropertyByName(request.XColumn);
            var y = query.FirstOrDefault().GetPropertyByName(request.YColumn);

            return query.Where(e => e.TraitId == trait.TraitId).Select(e => GetValues(e, x, y));            
        }

        private static Tuple<object, object> GetValues(object obj, PropertyInfo p1, PropertyInfo p2)
        {
            var x = p1.GetValue(obj);
            var y = p2.GetValue(obj);

            return new Tuple<object, object>(x, y);
        }
    }

}
