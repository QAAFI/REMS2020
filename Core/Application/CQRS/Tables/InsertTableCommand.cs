using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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

            // The DbSet for the entity type
            var set = _context.GetType()
                .GetMethod("GetSet")
                .MakeGenericMethod(request.Type)
                .Invoke(_context, new object[0])
                as IQueryable<IEntity>;

            // All the non-key properties of an entity
            var props = _context.GetEntityProperties(request.Type);

            IEntity entity = null;
            Func<IEntity, bool> matches = other =>
                    props.All(i => i.GetValue(entity)?.ToString().ToLower() == i.GetValue(other)?.ToString().ToLower());

            // Add all the rows as entities, if the data is not already in the DB
            foreach (DataRow row in request.Table.Rows)
            {
                entity = row.ToEntity(_context, request.Type, infos);                

                if (!set.Any() || !set.All(matches))
                    _context.Attach(entity);

                request.IncrementProgress();
            }            
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
