using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average value of a trait across a treatment on a date
    /// </summary>
    public class MeanDataQuery : ContextQuery<SeriesData<double, int>>
    {
        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<MeanDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<double, int> Run()
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.Plot.TreatmentId == TreatmentId)
                .Where(d => d.Date == Date)
                .Where(d => d.Trait.Name == TraitName)
                .ToArray() // Required to support groupby
                .GroupBy(d => d.DepthFrom)
                .OrderBy(g => g.Key)
                .ToArray();

            var series = new SeriesData<double, int>
            {
                Name = TraitName,
                X = data.Select(g => g.Average(d => d.Value)).ToArray(),
                Y = data.Select(g => g.Key).ToArray(),
                XName = "Value",
                YName = "Depth"
            };

            return series;
        }
    }
}
