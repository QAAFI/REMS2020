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
    /// Insert soil data into the database
    /// </summary>
    public class InsertSoilTableCommand : IRequest
    {
        /// <summary>
        /// The source data
        /// </summary>
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
            // Assume the first two columns do not contain trait data and 'skip' them
            int skip = 2;
            var traits = _context.GetTraitsFromColumns(request.Table, skip, "Soil");
            var entities = new List<IEntity>();

            foreach (DataRow row in request.Table.Rows)
            {
                // Assume the first column contains soil type data
                var soiltype = row[0].ToString();

                // Assume the second column contains notes
                var notes = row[1].ToString();

                // Check for a matching soil in the database
                var match = _context.Soils.SingleOrDefault(s => s.SoilType == soiltype && s.Notes == notes);
                
                // If no match was found, create a new soil
                var soil = match ?? new Soil{ SoilType = soiltype, Notes = notes };
                _context.Attach(soil);

                // Find the values of the traits
                traits.ForEach(trait =>
                {
                    // Do not store null values
                    var value = row[trait.Name];
                    if (value is DBNull) return;

                    // Look for an existing soil trait
                    var existing = _context.SoilTraits.SingleOrDefault(s => s.Trait == trait && s.Soil == soil);

                    // If no match exists, create a new one
                    var soiltrait = existing ?? new SoilTrait{ Trait = trait, Soil = soil };
                    
                    // Update the value
                    soiltrait.Value = Convert.ToDouble(value);
                    entities.Add(soiltrait);
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
