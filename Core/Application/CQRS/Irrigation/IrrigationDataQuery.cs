using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data on irrigation operations for a treatment
    /// </summary>
    public class IrrigationDataQuery : ContextQuery<SeriesData>
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
        protected override SeriesData Run()
        {
            var irrigations = _context.Irrigations
                .Where(i => i.TreatmentId == TreatmentId)
                .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), irrigations.Count()),
                Y = Array.CreateInstance(typeof(double), irrigations.Count()),
                XName = "Date",
                YName = "Amount",
                Name = "Irrigations"                
            };

            for (int i = 0; i < irrigations.Length; i++)
            {
                var item = irrigations[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Amount, i);
            }

            return data;
        }
    }
}
