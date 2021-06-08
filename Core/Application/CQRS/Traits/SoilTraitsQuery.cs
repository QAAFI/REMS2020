using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all soil traits in a treatment that have data
    /// </summary>
    public class SoilTraitsQuery : ContextQuery<string[]> 
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilTraitsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override string[] Run()
        {
            var traits = _context.Treatments.Find(TreatmentId)
                .Plots.SelectMany(p => p.SoilLayerData.Select(d => d.Trait.Name))
                .Distinct()
                .ToArray();

            return traits;
        }
    }
}
