using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the dates that soil layer data was measured on for a treatment
    /// </summary>
    public class SoilLayerDatesQuery : ContextQuery<DateTime[]>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilLayerDatesQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DateTime[] Run()
        {
            return _context.SoilLayerDatas
                .Where(d => d.Plot.TreatmentId == TreatmentId)
                .Select(d => d.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToArray();
        }
    }
}
