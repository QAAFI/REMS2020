using System;
using System.Linq;

using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Search for an entity type based on name
    /// </summary>
    public class EntityTypeQuery : ContextQuery<Type>
    {
        /// <summary>
        /// The name of the entity type to search for
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<EntityTypeQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Type Run()
        {
            var types = _context.Model.GetEntityTypes().ToArray();

            // If there is an exact name match, use that
            if (types.FirstOrDefault(t => t.ClrType.Name == Name) is IEntityType entity)
                return entity.ClrType;

            // Names might not match exactly - look for contains, not equality
            var filtered = types.Where(e => Name.Contains(e.ClrType.Name));

            // Assume that if only a single result is returned, that is the matching entity
            if (filtered.Count() == 1)
                return filtered.Single().ClrType;

            // Function to find the string which best matches the request
            IEntityType findMatch(IEntityType a, IEntityType b)
            {
                var x = Math.Abs(a.ClrType.Name.Length - Name.Length);
                var y = Math.Abs(b.ClrType.Name.Length - Name.Length);

                return x < y ? a : b;
            }

            return filtered.Aggregate(findMatch).ClrType;            
        }
    }
}