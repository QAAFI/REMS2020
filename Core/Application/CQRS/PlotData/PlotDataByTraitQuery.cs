using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds data for a trait in a plot
    /// </summary>
    public class PlotDataByTraitQuery : TraitDataQuery<DateTime, double>
    {
        /// <summary>
        /// The source plot
        /// </summary>
        public int PlotId { get; set; }
                
        /// <inheritdoc/>
        public class Handler : BaseHandler<PlotDataByTraitQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double> Run()
        {
            if (_context.Traits.FirstOrDefault(t => t.Name == TraitName) is not Trait trait)
                return null;

            if (_context.Plots.Find(PlotId) is not Plot plot)
                return null;

            DateTime[] dates;
            double[] values;
            
            if (trait.Type == "Soil")
            {
                var ordered = plot.SoilData.Where(d => d.Trait == trait).OrderBy(d => d.Date);

                dates = ordered.Select(d => d.Date).ToArray();
                values = ordered.Select(d => d.Value).ToArray();
            }
            else
            {
                var ordered = plot.PlotData.Where(d => d.Trait == trait).OrderBy(d => d.Date);

                dates = ordered.Select(d => d.Date).ToArray();
                values = ordered.Select(d => d.Value).ToArray();
            }       

            var rep = _context.Plots.Where(p => p.PlotId == PlotId);
            var x = rep.Select(p => p.Repetition).First();
            string name = TraitName + " " + x;

            string units = _context.Traits
                .FirstOrDefault(t => t.Name == TraitName)
                ?.Unit
                ?.Name;

            var series = new SeriesData<DateTime, double>
            {
                Name = name,
                X = dates,
                Y = values,
                XName = "Date",
                YName = $"Value ({units})"
            };

            return series;
        }
    }
}
