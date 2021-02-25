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
    public class InsertSoilLayerTraitsCommand : IRequest
    {
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public Type Dependency { get; set; }

        public Action IncrementProgress { get; set; }
    }

    public class InsertSoilLayerTraitsCommandHandler : IRequestHandler<InsertSoilLayerTraitsCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertSoilLayerTraitsCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertSoilLayerTraitsCommand request, CancellationToken cancellationToken)
            => Task.Run(() => Handler(request));

        private Unit Handler(InsertSoilLayerTraitsCommand request)
        {
            var traits = _context.GetTraitsFromColumns(request.Table, 3, "SoilLayer");

            // All the non-key properties of an entity
            var layer_props = _context.GetEntityProperties(typeof(SoilLayer));
            IEntity layer = null;
            Func<IEntity, bool> layer_matches = other =>
                    layer_props.All(i => i.GetValue(layer)?.ToString() == i.GetValue(other)?.ToString());

            var trait_props = _context.GetEntityProperties(typeof(SoilLayerTrait));
            IEntity trait = null;
            Func<IEntity, bool> trait_matches = other =>
                    trait_props.All(i => i.GetValue(trait)?.ToString() == i.GetValue(other)?.ToString());

            var entities = new List<IEntity>();

            foreach (DataRow r in request.Table.Rows)
            {
                layer = new SoilLayer
                {
                    Soil = _context.Soils.FirstOrDefault(s => s.SoilType == r[0].ToString()),
                    FromDepth = Convert.ToInt32(r[1]),
                    ToDepth = Convert.ToInt32(r[2])
                };

                if (_context.SoilLayers.SingleOrDefault(layer_matches) is SoilLayer soil)
                    layer = soil;
                else
                    _context.Attach(layer);

                var soils = traits.Select((t, i) => new SoilLayerTrait
                {
                    Trait = t,
                    SoilLayer = ((SoilLayer)layer)                    
                });

                

                soils.ForEach((t, i) => 
                {
                    trait = t;

                    var value = r[i + 3];

                    if (value is DBNull) return;

                    if (_context.SoilLayerTraits.SingleOrDefault(s => s.Trait == t.Trait && s.SoilLayer == (SoilLayer)layer) is SoilLayerTrait slt)
                        slt.Value = Convert.ToDouble(r[i + 3]);
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
