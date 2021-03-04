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
    public class InsertSoilTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public Action IncrementProgress { get; set; }
    }

    public class InsertSoilTableCommandHandler : IRequestHandler<InsertSoilTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertSoilTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertSoilTableCommand request, CancellationToken cancellationToken)
            => Task.Run(() => Handler(request));

        private Unit Handler(InsertSoilTableCommand request)
        {
            var traits = _context.GetTraitsFromColumns(request.Table, 2, "Soil");

            // All the non-key properties of an entity
            var soil_props = _context.GetEntityProperties(typeof(Soil));
            IEntity soil = null;
            Func<IEntity, bool> soil_matches = other =>
                    soil_props.All(i => i.GetValue(soil)?.ToString() == i.GetValue(other)?.ToString());

            //var trait_props = _context.GetEntityProperties(typeof(SoilTrait));
            IEntity trait = null;
            //Func<IEntity, bool> trait_matches = other =>
            //        trait_props.All(i => i.GetValue(trait)?.ToString() == i.GetValue(other)?.ToString());

            var entities = new List<IEntity>();

            foreach (DataRow r in request.Table.Rows)
            {
                soil = new Soil
                {
                    SoilType = r[0].ToString(),
                    Notes = r[1].ToString()
                };

                if (_context.Soils.SingleOrDefault(soil_matches) is Soil layer)
                    soil = layer;
                else
                    _context.Attach(soil);

                var soils = traits.Select((t, i) => new SoilTrait
                {
                    Trait = t,
                    Soil = ((Soil)soil),
                    Value = Convert.ToDouble(r[i + 2])
                });

                soils.ForEach((t, i) =>
                {
                    trait = t;

                    var value = r[i + 2];

                    if (value is DBNull) return;

                    if (_context.SoilTraits.SingleOrDefault(s => s.Trait == t.Trait && s.Soil == (Soil)soil) is SoilTrait slt)
                        slt.Value = Convert.ToDouble(value);
                    else
                        entities.Add(trait);
                });

                request.IncrementProgress();
            }
            _context.SaveChanges();

            // Add the traits once the soils have been saved
            _context.AttachRange(entities.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
