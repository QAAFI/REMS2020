using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Inserts the measured traits of soil layers into the database
    /// </summary>
    public class InsertSoilLayerTraitsCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source data
        /// </summary>
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public Type Dependency { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertSoilLayerTraitsCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            /* It is assumed that the table being imported uses the following schema:
             * Column 1: SoilType
             * Column 2: DepthFrom
             * Column 3: DepthTo
             * Column 4+: Traits
             * So we have to 'skip' 3 columns to get the traits
             */

            int skip = 3;
            var traits = _context.GetTraitsFromColumns(Table, skip, "SoilLayer");
            var entities = new List<SoilLayerTrait>();

            foreach (DataRow row in Table.Rows)
            {
                var soil = _context.Soils.FirstOrDefault(s => s.SoilType == row[0].ToString());
                
                if (!int.TryParse(row[1].ToString(), out int from))
                    throw new InvalidCastException("The SoilLayers table expects integer values for depth");

                if (!int.TryParse(row[2].ToString(), out int to))
                    throw new InvalidCastException("The SoilLayers table expects integer values for depth");

                var match = _context.SoilLayers.SingleOrDefault(s => s.Soil == soil && s.FromDepth == from && s.ToDepth == to);
                var layer = match ?? new SoilLayer { Soil = soil, FromDepth = from, ToDepth = to };
                _context.Attach(layer);

                traits.ForEach(trait =>
                {
                    var value = row[trait.Name];
                    if (value is DBNull) return;

                    var existing = _context.SoilLayerTraits.SingleOrDefault(s => s.Trait == trait && s.SoilLayer == layer);
                    var slt = existing ?? new SoilLayerTrait { Trait = trait, SoilLayer = layer };
                    slt.Value = Convert.ToDouble(value);
                    entities.Add(slt);
                });

                Progress.Increment(1);
            }
            _context.SaveChanges();

            // Can only add the traits once the soil layers have been saved
            _context.AttachRange(entities.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
