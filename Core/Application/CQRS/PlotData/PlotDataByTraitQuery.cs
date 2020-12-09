using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class PlotDataByTraitQuery : IRequest<SeriesData>
    {
        public int PlotId { get; set; }

        public string TraitName { get; set; }
    }

    public class PlotDataByTraitQueryHandler : IRequestHandler<PlotDataByTraitQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public PlotDataByTraitQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(PlotDataByTraitQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(PlotDataByTraitQuery request, CancellationToken token)
        {
            var data = _context.PlotData
                .Where(p => p.Plot.PlotId == request.PlotId)
                .Where(p => p.Trait.Name == request.TraitName)
                .OrderBy(p => p.Date)
                .ToArray();

            if (data.Length == 0) return null;

            var rep = _context.Plots.Where(p => p.PlotId == request.PlotId);
            var x = rep.Select(p => p.Repetition).First();
            string name = request.TraitName + " " + x;

            SeriesData series = new SeriesData()
            {
                Title = name,
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
