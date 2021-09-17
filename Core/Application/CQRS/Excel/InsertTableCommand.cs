using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Insert a table of data into the database
    /// </summary>
    public class InsertTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source data
        /// </summary>
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            // All the property infos for an entity
            var infos = Table.Columns.Cast<DataColumn>()
                    .Select(c => c.FindProperty())
                    .Where(i => i != null)
                    .ToArray();

            // The DbSet for the entity type
            var set = _context.GetType()
                .GetMethod(nameof(_context.GetSet))
                .MakeGenericMethod(Type)
                .Invoke(_context, new object[0])
                as IQueryable<IEntity>;

            // All the non-key properties of an entity
            var props = _context.GetEntityProperties(Type);

            IEntity entity = null;

            // Test if the given entity matches the tracked entity for the non-key properties
            bool matches(IEntity other) => props.All(i => PropertiesMatch(i, entity, other));

            // Add all the rows as entities, if the data is not already in the DB
            foreach (DataRow row in Table.Rows)
            {
                entity = row.ToEntity(_context, Type, infos);
                bool test = set.Any(matches);

                if (!set.Any(matches))
                    _context.Attach(entity);

                Progress.Report(1);
            }
            _context.SaveChanges();

            return Unit.Value;            
        }

        private static bool PropertiesMatch(PropertyInfo info, IEntity entity, IEntity other)
        {
            var x = info.GetValue(entity)?.ToString().ToLower();
            var y = info.GetValue(other)?.ToString().ToLower();

            return x == y;
        }
    }
}