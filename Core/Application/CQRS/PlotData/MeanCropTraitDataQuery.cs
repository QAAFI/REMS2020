using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average values for a trait across a treatment
    /// </summary>
    public class MeanCropTraitDataQuery : ContextQuery<SeriesData<DateTime, double>>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<MeanCropTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double> Run()
        {
            var data = _context.PlotData
                .Where(p => p.Plot.TreatmentId == TreatmentId)
                .Where(p => p.Trait.Name == TraitName)
                .ToArray() // Have to cast to an array to support the following GroupBy
                .GroupBy(p => p.Date)
                .OrderBy(g => g.Key)
                .ToArray();

            var series = new SeriesData<DateTime, double>
            {
                Name = TraitName,
                X = data.Select(g => g.Key).ToArray(),
                Y = data.Select(g => g.Average(d => d.Value)).ToArray(),
                XName = "Date",
                YName = "Value"
            };

            return series;
        }
    }
}
