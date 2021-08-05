using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average value of a soil trait in a treatment for a given date
    /// </summary>
    public class MeanSoilTraitDataQuery : TraitDataQuery<double, int>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<MeanSoilTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<double, int> Run()
        {
            var data = _context.SoilLayerDatas
                .Where(p => p.Plot.TreatmentId == TreatmentId)
                .Where(p => p.Trait.Name == TraitName)                
                .Where(p => p.Date == Date)
                .ToArray()
                .GroupBy(p => p.DepthFrom)
                .OrderBy(g => g.Key)
                .ToArray();

            var series = new SeriesData<double, int>
            {
                Name = TraitName,                
                X = data.Select(g => g.Average(p => p.Value)).ToArray(),
                Y = data.Select(g => g.Key).ToArray(),
                XName = "Value",
                YName = "Depth"
            };

            return series;
        }
    }
}
