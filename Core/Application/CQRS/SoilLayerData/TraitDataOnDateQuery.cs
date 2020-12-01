using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class TraitDataOnDateQuery : ITraitQuery<SeriesData>
    {
        public DateTime Date { get; set; }

        public int PlotId { get; set; }

        public string TraitName { get; set; }
    }

    public class TraitDataOnDateQueryHandler : IRequestHandler<TraitDataOnDateQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public TraitDataOnDateQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(TraitDataOnDateQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(TraitDataOnDateQuery request, CancellationToken token)
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.PlotId == request.PlotId)
                .Where(d => d.Date == request.Date)
                .Where(d => d.Trait.Name == request.TraitName)
                .OrderBy(d => d.DepthFrom)
                .ToArray();

            var rep = _context.Plots.Where(p => p.PlotId == request.PlotId);
            var x = rep.Select(p => p.Repetition).First();
            string name = request.TraitName + " " + x;

            SeriesData series = new SeriesData()
            {
                Title = name,
                X = Array.CreateInstance(typeof(double), data.Count()),
                Y = Array.CreateInstance(typeof(int), data.Count()),
                XLabel = "Value",
                YLabel = "Depth"
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
