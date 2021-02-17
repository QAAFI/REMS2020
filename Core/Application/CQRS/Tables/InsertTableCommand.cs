using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    public class InsertTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public Action IncrementProgress { get; set; }
    }

    public class InsertTableCommandHandler : IRequestHandler<InsertTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertTableCommand request, CancellationToken cancellationToken) 
            => Task.Run(() => Handler(request));

        private Unit Handler(InsertTableCommand request)
        {
            // All the property infos for an entity
            var infos = request.Table.Columns.Cast<DataColumn>()
                .Select(c => c.FindProperty())
                .Where(i => i != null)
                .ToArray();

            // All the primary and foreign keys for an entity
            var type = _context.Model.GetEntityTypes()
                .First(e => e.ClrType == request.Type);

            var primaries = type
                .FindPrimaryKey()
                .Properties
                .Select(p => p.PropertyInfo);

            var foreigns = type
                .GetForeignKeys()
                .SelectMany(k => k.Properties.Select(p => p.PropertyInfo));

            // All the non-primary fields of an entity
            var fields = request.Type
                .GetProperties()
                .Where(p => !p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.IsValueType || p.PropertyType.IsInstanceOfType(""))
                .Except(primaries)
                .Except(foreigns)
                .ToArray();

            // The DbSet for the entity type
            var set = _context.GetType()
                .GetMethod("GetSet")
                .MakeGenericMethod(request.Type)
                .Invoke(_context, new object[0])
                as IEnumerable<IEntity>;

            // A check for if the context contains an entity
            bool contains(IEntity entity)
            {
                foreach (var item in set)
                    if (entity.Matches(item, fields))
                        return true;

                return false;
            }

            // Add all the rows as entities, if the data is not already in the DB
            foreach (DataRow row in request.Table.Rows)
            {
                var entity = row.ToEntity(_context, request.Type, infos);
                
                if (!contains(entity))
                    _context.Add(entity);

                request.IncrementProgress();
            }            
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
