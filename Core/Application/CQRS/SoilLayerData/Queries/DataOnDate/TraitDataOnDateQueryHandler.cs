using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using System.Linq;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class TraitDataOnDateQueryHandler : IRequestHandler<TraitDataOnDateQuery, SeriesData>
    {
        private readonly IRemsDbFactory factory;

        public TraitDataOnDateQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<SeriesData> Handle(TraitDataOnDateQuery request, CancellationToken token)
        {
            var data = factory.Context.SoilLayerDatas
                .Where(d => d.PlotId == request.PlotId)
                .Where(d => d.Date == request.Date)
                .Where(d => d.Trait.Name == request.TraitName)
                .OrderBy(d => d.DepthFrom)
                .ToArray();

            var rep = factory.Context.Plots.Where(p => p.PlotId == request.PlotId);
            var x = rep.Select(p => p.Repetition).First();
            string name = request.TraitName + " " + x;

            SeriesData series = new SeriesData()
            {
                Name = name,
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