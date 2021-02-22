using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    public class InsertTraitTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public Type Dependency { get; set; }

        public Action IncrementProgress { get; set; }
    }

    public class InsertTraitTableCommandHandler : IRequestHandler<InsertTraitTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertTraitTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertTraitTableCommand request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));

        private Unit Handler(InsertTraitTableCommand request)
        {
            GetTraits(request, out PropertyInfo[] infos, out Trait[] traits);            

            var foreignInfo = request.Dependency.GetProperties().First(p => p.PropertyType == request.Type);
            var traitInfo = request.Dependency.GetProperty("TraitId");
            var valueInfo = request.Dependency.GetProperty("Value");

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
                    props.All(i => i.GetValue(entity)?.ToString() == i.GetValue(other)?.ToString());

            var entities = new List<IEntity>();
            foreach (DataRow r in request.Table.Rows)
            {
                entity = r.ToEntity(_context, request.Type, infos.ToArray());

                foreach (var trait in traits)
                {
                    var foreign = Activator.CreateInstance(foreignInfo.DeclaringType) as IEntity;
                    var value = r[trait.Name];

                    foreignInfo.SetValue(foreign, entity);
                    traitInfo.SetValue(foreign, trait.TraitId);
                    foreign.SetValue(valueInfo, value);
                    entities.Add(foreign);
                }

                if (!set.Any() || !set.All(matches))
                    _context.Attach(entity);

                request.IncrementProgress();
            }
            _context.SaveChanges();

            var fset = _context.GetType()
                .GetMethod("GetSet")
                .MakeGenericMethod(foreignInfo.DeclaringType)
                .Invoke(_context, new object[0])
                as IQueryable<IEntity>;

            foreach (var e in entities)
            {
                entity = e;

                if (!fset.Any() || !fset.All(matches))
                    _context.Attach(entity);
            }            
            _context.SaveChanges();
            return Unit.Value;
        }

        private void GetTraits(InsertTraitTableCommand request, out PropertyInfo[] infos, out Trait[] traits)
        {
            var i = new List<PropertyInfo>();
            var t = new List<Trait>();

            foreach (DataColumn c in request.Table.Columns)
            {
                if (c.ColumnName.Contains("Column")) continue;

                if (c.FindProperty() is PropertyInfo info)
                {
                    i.Add(info);
                }
                else
                {
                    var trait = _context.Traits.FirstOrDefault(e => e.Name == c.ColumnName);
                    if (trait is null) trait = _context.AddTrait(c.ColumnName, request.Type.Name);

                    t.Add(trait);
                }
            }

            infos = i.ToArray();
            traits = t.ToArray();
        }
    }
}
