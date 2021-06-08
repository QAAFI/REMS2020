using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data for each plot in a treatment for a given trait
    /// </summary>
    public class AllCropTraitDataQuery : ContextQuery<SeriesData<DateTime, double>[]>
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
        public class Handler : BaseHandler<AllCropTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double>[] Run()
        {
            var plots = _context.Treatments.Find(TreatmentId).Plots;

            return plots.Select(plot => GetPlotData(plot.PlotId, TraitName)).ToArray();
        }

        private SeriesData<DateTime, double> GetPlotData(int id, string trait)
        {
            var data = _context.PlotData
                .Where(p => p.Plot.PlotId == id)
                .Where(p => p.Trait.Name == trait)
                .OrderBy(p => p.Date)
                .ToArray();

            if (data.Length == 0) return null;

            var rep = _context.Plots.Where(p => p.PlotId == id);
            var x = rep.Select(p => p.Repetition).First();
            string name = trait + " " + x;

            var series = new SeriesData<DateTime, double>
            {
                Name = name,
                X = data.Select(d => d.Date).ToArray(),
                Y = data.Select(d => d.Value).ToArray(),
                XName = "Value",
                YName = "Date"
            };

            for (int i = 0; i < data.Count(); i++)
            {
                series.X.SetValue(data[i].Date, i);
                series.Y.SetValue(data[i].Value, i);
            }

            return series;
        }
    }
}
