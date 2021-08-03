using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average values for a trait across a treatment
    /// </summary>
    public class MeanCropTraitDataQuery : TraitDataQuery<DateTime, double>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<MeanCropTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double> Run()
        {
            if (_context.Traits.FirstOrDefault(t => t.Name == TraitName) is not Trait trait)
                return null;

            // Have to cast to an array to support the following GroupBy
            IEnumerable<(DateTime Date, double Value)> arr = trait.Type == "Soil"
                ? _context.SoilDatas
                    .Where(p => p.Plot.TreatmentId == TreatmentId)
                    .Where(p => p.Trait.Name == TraitName)
                    .ToArray()
                    .Select(p => (p.Date, p.Value))
                : _context.PlotData
                    .Where(p => p.Plot.TreatmentId == TreatmentId)
                    .Where(p => p.Trait.Name == TraitName)
                    .ToArray()
                    .Select(p => (p.Date, p.Value));

            var data = arr.GroupBy(p => p.Date)
                .OrderBy(g => g.Key);

            string units = _context.Traits
                .FirstOrDefault(t => t.Name == TraitName)
                ?.Unit
                ?.Name;

            var series = new SeriesData<DateTime, double>
            {
                Name = TraitName,
                X = data.Select(g => g.Key).ToArray(),
                Y = data.Select(g => g.Average(d => d.Value)).ToArray(),
                XName = "Date",
                YName = $"Value ({units})"
            };

            return series;
        }
    }
}
