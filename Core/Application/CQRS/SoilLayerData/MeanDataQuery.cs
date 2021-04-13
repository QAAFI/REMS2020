using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average value of a trait across a treatment on a date
    /// </summary>
    public class MeanDataQuery : ContextQuery<SeriesData>
    {
        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<MeanDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData Run()
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.Plot.TreatmentId == TreatmentId)
                .Where(d => d.Date == Date)
                .Where(d => d.Trait.Name == TraitName)
                .ToArray() // Required to support groupby
                .GroupBy(d => d.DepthFrom)
                .OrderBy(g => g.Key)
                .ToArray();

            SeriesData series = new SeriesData()
            {
                Name = TraitName,
                X = Array.CreateInstance(typeof(double), data.Count()),
                Y = Array.CreateInstance(typeof(int), data.Count()),
                XName = "Value",
                YName = "Depth"
            };

            for (int i = 0; i < data.Count(); i++)
            {
                var group = data[i];
                var value = group.Average(p => p.Value);

                series.X.SetValue(value, i);
                series.Y.SetValue(group.Key, i);
            }

            return series;
        }
    }
}
