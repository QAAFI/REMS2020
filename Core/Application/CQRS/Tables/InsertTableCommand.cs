using System;
using System.Data;
using System.Linq;
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

        public Action IncrementProgress { get; set; }

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
            Func<IEntity, bool> matches = other =>
                    props.All(i => i.GetValue(entity)?.ToString().ToLower() == i.GetValue(other)?.ToString().ToLower());

            // Add all the rows as entities, if the data is not already in the DB
            foreach (DataRow row in Table.Rows)
            {
                entity = row.ToEntity(_context, Type, infos);

                if (!set.Any() || !set.All(matches))
                    _context.Attach(entity);

                IncrementProgress();
            }
            _context.SaveChanges();

            return Unit.Value;            
        }
    }
}