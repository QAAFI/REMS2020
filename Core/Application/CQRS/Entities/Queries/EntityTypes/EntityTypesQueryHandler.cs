using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.CQRS
{
    public class EntityTypesQueryHandler : IRequestHandler<EntityTypesQuery, Type>
    {
        private readonly IRemsDbContext _context;

        public EntityTypesQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Type> Handle(EntityTypesQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => Handler(request));
        }

        private Type Handler(EntityTypesQuery request)
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

            var args = new ItemNotFoundArgs() { Name = request.Name };

            // If there is no match, get a list of the possible types
            if (filtered.Count() == 0)
                args.Options = types.Select(t => t.ClrType.Name).ToArray();
            // If there are multiple options left, get a list of those options
            else
                args.Options = filtered.Select(t => t.ClrType.Name).ToArray();

            // Ask the user to pick an entity type from the list of options
            EventManager.InvokeItemNotFound(null, args);

            if (args.Cancelled || args.Selection == "None")
                return null;

            return types.First(t => t.ClrType.Name == args.Selection).ClrType;
        }
    }
}
