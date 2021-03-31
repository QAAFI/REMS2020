using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

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
            var traits = _context.Treatments.Find(TreatmentId)
                .Plots.SelectMany(p => p.PlotData.Select(d => d.Trait.Name))
                .Distinct()
                .ToArray();

            return traits;
        }
    }
}
