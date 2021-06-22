using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find soil trait data for all plots in a treatment on a given date
    /// </summary>
    public class AllSoilTraitDataQuery : ContextQuery<SeriesData<double, int>[]>
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
        protected override SeriesData<double, int>[] Run()
        {
            var data = _context.Treatments
                .Find(TreatmentId)
                .Plots
                .Select(p => GetPlotData(p.PlotId, Date, TraitName))
                .ToArray();

            return data;
        }

        private SeriesData<double, int> GetPlotData(int id, DateTime date, string trait)
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

            var series = new SeriesData<double, int>
            {
                Name = name,
                X = data.Select(d => d.Value).ToArray(),
                Y = data.Select(d => d.DepthTo).ToArray(),
                XName = "Value",
                YName = "Depth"
            };

            return series;
        }
    }
}
