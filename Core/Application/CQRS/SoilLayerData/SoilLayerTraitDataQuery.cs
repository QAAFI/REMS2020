using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the data for a soil layer trait in a plot on the given date
    /// </summary>
    public class SoilLayerTraitDataQuery : IRequest<SeriesData>
    {
        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The source plot
        /// </summary>
        public int PlotId { get; set; }

        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }
    }

    public class SoilLayerTraitDataQueryHandler : IRequestHandler<SoilLayerTraitDataQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public SoilLayerTraitDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(SoilLayerTraitDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(SoilLayerTraitDataQuery request, CancellationToken token)
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.PlotId == request.PlotId)
                .Where(d => d.Date == request.Date)
                .Where(d => d.Trait.Name == request.TraitName)
                .OrderBy(d => d.DepthFrom)
                .ToArray();

            var plot = _context.Plots.Find(request.PlotId);
            var x = plot.Repetition.ToString();
            string name = x + " " + request.TraitName + ", " + request.Date.ToString("dd/MM/yy");

            SeriesData series = new SeriesData()
            {
                Name = name,
                X = Array.CreateInstance(typeof(double), data.Count()),
                Y = Array.CreateInstance(typeof(int), data.Count()),
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
