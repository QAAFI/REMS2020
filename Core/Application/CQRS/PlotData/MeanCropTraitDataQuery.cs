using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average values for a trait across a treatment
    /// </summary>
    public class MeanCropTraitDataQuery : ContextQuery<SeriesData>
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
        public class Handler : BaseHandler<MeanCropTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData Run()
        {
            var data = _context.PlotData
                .Where(p => p.Plot.TreatmentId == TreatmentId)
                .Where(p => p.Trait.Name == TraitName)
                .ToArray() // Have to cast to an array to support the following GroupBy
                .GroupBy(p => p.Date)
                .OrderBy(g => g.Key)
                .ToArray();

            SeriesData series = new SeriesData()
            {
                Name = TraitName,
                X = Array.CreateInstance(typeof(DateTime), data.Count()),
                Y = Array.CreateInstance(typeof(double), data.Count()),
                XName = "Date",
                YName = "Value"
            };

            for (int i = 0; i < data.Count(); i++)
            {
                var group = data[i];
                var value = group.Average(p => p.Value);

                series.X.SetValue(group.Key, i);
                series.Y.SetValue(value, i);
            }

            return series;
        }
    }
}
