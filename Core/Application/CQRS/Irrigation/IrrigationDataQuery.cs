using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data on irrigation operations for a treatment
    /// </summary>
    public class IrrigationDataQuery : ContextQuery<SeriesData<DateTime, double>>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<IrrigationDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double> Run()
        {
            var irrigations = _context.Irrigations
                .Where(i => i.TreatmentId == TreatmentId)
                .ToArray();

            var data = new SeriesData<DateTime, double>
            {
                X = irrigations.Select(i => i.Date).ToArray(),
                Y = irrigations.Select(i => i.Amount).ToArray(),
                XName = "Date",
                YName = "Amount",
                Name = "Irrigations"
            };

            return data;
        }
    }
}
