using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using System.Collections.Generic;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find soil trait data for all plots in a treatment on a given date
    /// </summary>
    public class AllSoilTraitDataQuery : ContextQuery<IEnumerable<SeriesData>>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }

        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<AllSoilTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override IEnumerable<SeriesData> Run()
        {
            var plots = _context.Treatments.Find(TreatmentId).Plots;

            foreach (var plot in plots)
                yield return GetPlotData(plot.PlotId, Date, TraitName);
        }

        private SeriesData GetPlotData(int id, DateTime date, string trait)
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.PlotId == id)
                .Where(d => d.Date == date)
                .Where(d => d.Trait.Name == trait)
                .OrderBy(d => d.DepthFrom)
                .ToArray();

            var plot = _context.Plots.Find(id);
            var x = plot.Repetition.ToString();
            string name = x + " " + trait + ", " + date.ToString("dd/MM/yy");

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
