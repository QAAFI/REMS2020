using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Request data on tillage operations for a treatment
    /// </summary>
    public class TillagesDataQuery : ContextQuery<SeriesData>
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
        protected override SeriesData Run()
        {
            var tillages = _context.Tillages
                .Where(i => i.TreatmentId == TreatmentId)
                .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), tillages.Count()),
                Y = Array.CreateInstance(typeof(double), tillages.Count()),
                XName = "Date",
                YName = "Depth",
                Name = "Tillages"
            };

            for (int i = 0; i < tillages.Length; i++)
            {
                var item = tillages[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Depth, i);
            }

            return data;
        }
    }
}
