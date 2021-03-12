using System;
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
    /// <summary>
    /// Inserts the measured traits of soil layers into the database
    /// </summary>
    public class InsertSoilLayerTraitsCommand : IRequest
    {
        /// <summary>
        /// The source data
        /// </summary>
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
            /* It is assumed that the table being imported uses the following schema:
             * Column 1: SoilType
             * Column 2: DepthFrom
             * Column 3: DepthTo
             * Column 4+: Traits
             * So we have to 'skip' 3 columns to get the traits
             */

            int skip = 3;
            var traits = _context.GetTraitsFromColumns(request.Table, skip, "SoilLayer");
            var entities = new List<SoilLayerTrait>();

            foreach (DataRow row in request.Table.Rows)
            {
                var soil = _context.Soils.FirstOrDefault(s => s.SoilType == row[0].ToString());
                int from = Convert.ToInt32(row[1]);
                int to = Convert.ToInt32(row[2]);

                var match = _context.SoilLayers.SingleOrDefault(s => s.Soil == soil && s.FromDepth == from && s.ToDepth == to);
                var layer = match ?? new SoilLayer { Soil = soil, FromDepth = from, ToDepth = to };
                _context.Attach(layer);

                traits.ForEach(trait => 
                {
                    var value = row[trait.Name];
                    if (value is DBNull) return;

                    var existing = _context.SoilLayerTraits.SingleOrDefault(s => s.Trait == trait && s.SoilLayer == layer);
                    var slt = existing ?? new SoilLayerTrait{ Trait = trait, SoilLayer = layer };
                    slt.Value = Convert.ToDouble(value);
                    entities.Add(slt);
                });                

                request.IncrementProgress();
            }
            _context.SaveChanges();

            // Can only add the traits once the soil layers have been saved
            _context.AttachRange(entities.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
