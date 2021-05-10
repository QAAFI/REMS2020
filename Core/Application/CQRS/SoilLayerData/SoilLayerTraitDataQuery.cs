using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the data for a soil layer trait in a plot on the given date
    /// </summary>
    public class SoilLayerTraitDataQuery : ContextQuery<SeriesData>
    {
        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The source plot
        /// </summary>
        public int PlotId { get; set; }

        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilLayerTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData Run()
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.PlotId == PlotId)
                .Where(d => d.Date == Date)
                .Where(d => d.Trait.Name == TraitName)
                .OrderBy(d => d.DepthFrom)
                .ToArray();

            var plot = _context.Plots.Find(PlotId);
            var x = plot.Repetition.ToString();
            string name = x + " " + TraitName + ", " + Date.ToString("dd/MM/yy");

            SeriesData series = new SeriesData()
            {
                Name = name,
                X = new double[data.Count()],
                Y = new double[data.Count()],
                //X = Array.CreateInstance(typeof(double), data.Count()),
                //Y = Array.CreateInstance(typeof(int), data.Count()),
                XName = "Value",
                YName = "Depth"
            };

            for (int i = 0; i < data.Count(); i++)
            {
                var soil = data[i];

                series.X.SetValue(soil.Value, i);
                series.Y.SetValue(soil.DepthTo, i);
            }

            return series;
        }
    }
}
