using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data on fertilization operations for a treatment
    /// </summary>
    public class FertilizationDataQuery : ContextQuery<SeriesData>
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
        protected override SeriesData Run()
        {
            var fertilizations = _context.Fertilizations
                 .Where(i => i.TreatmentId == TreatmentId)
                 .ToArray();

            var data = new SeriesData()
            {
                X = new double[fertilizations.Count()],
                Y = new double[fertilizations.Count()],
                //X = Array.CreateInstance(typeof(DateTime), fertilizations.Count()),
                //Y = Array.CreateInstance(typeof(double), fertilizations.Count()),
                XName = "Date",
                YName = "Amount",
                Name = "Fertilizations"
            };

            for (int i = 0; i < fertilizations.Length; i++)
            {
                var item = fertilizations[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Amount, i);
            }

            return data;
        }
    }
}
