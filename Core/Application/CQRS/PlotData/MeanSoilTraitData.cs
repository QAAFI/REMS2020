using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average value of a soil trait in a treatment for a given date
    /// </summary>
    public class MeanSoilTraitDataQuery : ContextQuery<SeriesData>
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
        public class Handler : BaseHandler<MeanSoilTraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData Run()
        {
            var data = _context.SoilLayerDatas
                .Where(p => p.Plot.TreatmentId == TreatmentId)
                .Where(p => p.Trait.Name == TraitName)                
                .Where(p => p.Date == Date)
                .ToArray()
                .GroupBy(p => p.DepthFrom)
                .OrderBy(g => g.Key)
                .ToArray();

            string name = TraitName + ", " + Date.ToString("dd/MM/yy");

            SeriesData series = new SeriesData()
            {
                Name = name,
                X = data.Select(g => g.Average(p => p.Value)).ToArray(),
                Y = data.Select(g => (double)g.Key).ToArray(),
                XName = "Value",
                YName = "Depth"
            };

            //for (int i = 0; i < data.Count(); i++)
            //{
            //    var group = data[i];
            //    var value = group.Average(p => p.Value);

            //    series.X.SetValue(value, i);
            //    series.Y.SetValue(group.Key, i);
            //}

            return series;
        }
    }
}
