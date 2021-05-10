using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using System.Collections.Generic;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data for each plot in a treatment for a given trait
    /// </summary>
    public class AllCropTraitDataQuery : ContextQuery<IEnumerable<SeriesData>>
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
        protected override IEnumerable<SeriesData> Run()
        {
            var plots = _context.Treatments.Find(TreatmentId).Plots;

            foreach (var plot in plots)
                yield return GetPlotData(plot.PlotId, TraitName);
        }

        private SeriesData GetPlotData(int id, string trait)
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

            SeriesData series = new SeriesData()
            {
                Name = name,
                X = new double[data.Count()],
                Y = new double[data.Count()],
                //X = Array.CreateInstance(typeof(DateTime), data.Count()),
                //Y = Array.CreateInstance(typeof(double), data.Count()),
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
