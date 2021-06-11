using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the crop traits with data in a treatment
    /// </summary>
    public class CropTraitsQuery : ContextQuery<string[]> 
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<CropTraitsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override string[] Run()
        {
            IEnumerable<string> getTraits(Plot p)
            {
                var x = p.SoilData.Select(d => d.Trait.Name);
                var y = p.PlotData.Select(d => d.Trait.Name);

                return x.Union(y);
            };

            var traits = _context.Treatments.Find(TreatmentId)
                .Plots.SelectMany(p => getTraits(p))
                .Distinct()
                .ToArray();

            return traits;
        }
    }
}
