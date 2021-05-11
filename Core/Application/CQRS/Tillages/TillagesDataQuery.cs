using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Request data on tillage operations for a treatment
    /// </summary>
    public class TillagesDataQuery : ContextQuery<SeriesData<DateTime, double>>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<TillagesDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double> Run()
        {
            var tillages = _context.Tillages
                .Where(i => i.TreatmentId == TreatmentId)
                .ToArray();

            var data = new SeriesData<DateTime, double>
            {
                X = tillages.Select(t => t.Date).ToArray(),
                Y = tillages.Select(t => t.Depth).ToArray(),
                XName = "Date",
                YName = "Depth",
                Name = "Tillages"
            };

            return data;
        }
    }
}
