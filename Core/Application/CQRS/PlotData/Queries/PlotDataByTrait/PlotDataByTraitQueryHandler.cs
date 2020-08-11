using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;
using Rems.Application.Common;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlotDataByTraitQueryHandler : IRequestHandler<PlotDataByTraitQuery, SeriesData>
    {
        private readonly IRemsDbFactory factory;

        public PlotDataByTraitQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<SeriesData> Handle(PlotDataByTraitQuery request, CancellationToken token)
        {
            var data = factory.Context.PlotData
                .Where(p => p.Plot.PlotId == request.PlotId)
                .Where(p => p.Trait.Name == request.TraitName)
                .OrderBy(p => p.Date)
                .ToArray();

            var rep = factory.Context.Plots.Where(p => p.PlotId == request.PlotId);
            var x = rep.Select(p => p.Repetition).First();
            string name = request.TraitName + " " + x;

            SeriesData series = new SeriesData()
            {
                Name = name,
                X = Array.CreateInstance(typeof(DateTime), data.Count()),
                Y = Array.CreateInstance(typeof(double), data.Count()),
                XLabel = "Value",
                YLabel = "Date"
            };

            for (int i = 0; i < data.Count(); i++)
            {
                series.X.SetValue(data[i].Date, i);
                series.Y.SetValue(data[i].Value, i);
            }

            return series;            
        }
    }
}