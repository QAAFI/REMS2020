using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class EntityTypeQuery : IRequest<Type>
    {
        public string Name { get; set; }

        public RequestItem GetItem { get; set; }
    }

    public class EntityTypeQueryHandler : IRequestHandler<EntityTypeQuery, Type>
    {
        private readonly IRemsDbContext _context;

        public EntityTypeQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Type> Handle(EntityTypeQuery request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));

        private Type Handler(EntityTypeQuery request)
        {
            var types = _context.Model.GetEntityTypes().ToArray();

            // If there is an exact name match, use that
            if (types.FirstOrDefault(t => t.ClrType.Name == request.Name) is IEntityType entity)
                return entity.ClrType;

            // Names might not match exactly - look for contains, not equality
            var filtered = types.Where(e => request.Name.Contains(e.ClrType.Name));

            // Assume that if only a single result is returned, that is the matching entity
            if (filtered.Count() == 1)
                return filtered.Single().ClrType;

            //var args = new ItemNotFoundArgs() { Name = request.Name };

            //// If there is no match, get a list of the possible types
            //if (filtered.Count() == 0)
            //    args.Options = types.Select(t => t.ClrType.Name).ToArray();
            //// If there are multiple options left, get a list of those options
            //else
            //    args.Options = filtered.Select(t => t.ClrType.Name).ToArray();

            // 
            var item = request.GetItem(request.Name);
            return types.FirstOrDefault(t => t.ClrType.Name == item).ClrType;
        }
    }
}
