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
            IEnumerable<(DateTime Date, double Value)> arr;

            switch (trait.Type)
            {
                case "Climate":
                    var exp = _context.Treatments.Find(TreatmentId).Experiment;
                    arr = exp.MetStation.MetData
                        .Where(p => p.Trait.Name == TraitName)
                        .GroupBy(m => (m.Date.Day, m.Date.Month))
                        .Select(g => (
                            new DateTime(2020, g.Key.Month, g.Key.Day),
                            g.Select(m => m.Value).Average()
                        ));
                    break;

                case "Soil":
                    arr = _context.SoilDatas
                        .Where(p => p.Plot.TreatmentId == TreatmentId)
                        .Where(p => p.Trait.Name == TraitName)
                        .ToArray()
                        .Select(p => (p.Date, p.Value));
                    break;

                case "Crop":
                default:
                    arr = _context.PlotData
                        .Where(p => p.Plot.TreatmentId == TreatmentId)
                        .Where(p => p.Trait.Name == TraitName)
                        .ToArray()
                        .Select(p => (p.Date, p.Value));
                    break;
            }

            var data = arr.GroupBy(p => p.Date).OrderBy(g => g.Key);
            string units = trait.Unit?.Name;

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
