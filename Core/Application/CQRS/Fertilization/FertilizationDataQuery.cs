using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data on fertilization operations for a treatment
    /// </summary>
    public class FertilizationDataQuery : ContextQuery<SeriesData<DateTime, double>>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<FertilizationDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double> Run()
        {
            var fertilizations = _context.Fertilizations
                 .Where(i => i.TreatmentId == TreatmentId)
                 .ToArray();

            var data = new SeriesData<DateTime, double>
            {
                X = fertilizations.Select(f => f.Date).ToArray(),
                Y = fertilizations.Select(f => f.Amount).ToArray(),
                XName = "Date",
                YName = "Amount",
                Name = "Fertilizations"
            };

            return data;
        }
    }
}
