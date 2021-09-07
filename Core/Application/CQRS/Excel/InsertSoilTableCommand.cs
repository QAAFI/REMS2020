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
    /// Insert soil data into the database
    /// </summary>
    public class InsertSoilTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source data
        /// </summary>
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertSoilTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            /* It is assumed that the table being imported uses the following schema:
             * Column 1: SoilType
             * Column 2: Notes
             * Column 3+: Traits
             * So we have to 'skip' 2 columns to get the traits
             */
            
            int skip = 2;
            var traits = _context.GetTraitsFromColumns(Table, skip, "Soil");
            var entities = new List<IEntity>();

            foreach (DataRow row in Table.Rows)
            {
                var soiltype = row[0].ToString();
                var notes = row[1].ToString();

                var match = _context.Soils.SingleOrDefault(s => s.SoilType == soiltype && s.Notes == notes);
                var soil = match ?? new Soil { SoilType = soiltype, Notes = notes };
                _context.Attach(soil);

                traits.ForEach(trait =>
                {
                    if (row[trait.Name] is DBNull) return;

                    var existing = _context.SoilTraits.SingleOrDefault(s => s.Trait == trait && s.Soil == soil);
                    var soiltrait = existing ?? new SoilTrait { Trait = trait, Soil = soil };
                    soiltrait.Value = row.GetDouble(trait.Name);
                    entities.Add(soiltrait);
                });

                Progress.Increment(1);
            }
            _context.SaveChanges();

            // Can only add the traits once the soils have been saved
            _context.AttachRange(entities.ToArray());
            _context.SaveChanges();

            return Unit.Value;            
        }
    }
}
