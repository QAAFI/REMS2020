using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the data for a soil layer trait in a plot on the given date
    /// </summary>
    public class SoilLayerTraitDataQuery : TraitDataQuery<double, int>
    {
        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The source plot
        /// </summary>
        public int PlotId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilLayerTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<double, int> Run()
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.PlotId == PlotId)
                .Where(d => d.Date == Date)
                .Where(d => d.Trait.Name == TraitName)
                .OrderBy(d => d.DepthFrom)
                .ToArray();

            var plot = _context.Plots.Find(PlotId);

            var series = new SeriesData<double, int>
            {
                Name = TraitName + " " + Date.ToString("dd-MMM-yy"),
                Series = plot.Repetition,
                X = data.Select(d => d.Value).ToArray(),
                Y = data.Select(d => d.DepthTo).ToArray(),
                XName = "Value",
                YName = "Depth"
            };

            return series;
        }
    }
}
