using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class MeanDataQuery : IRequest<SeriesData>
    {
        public DateTime Date { get; set; }

        public int TreatmentId { get; set; }

        public string TraitName { get; set; }
    }

    public class MeanDataQueryHandler : IRequestHandler<MeanDataQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public MeanDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(MeanDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(MeanDataQuery request, CancellationToken token)
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.Plot.TreatmentId == request.TreatmentId)
                .Where(d => d.Date == request.Date)
                .Where(d => d.Trait.Name == request.TraitName)
                .ToArray() // Required to support groupby
                .GroupBy(d => d.DepthFrom)
                .OrderBy(g => g.Key)
                .ToArray();

            SeriesData series = new SeriesData()
            {
                Title = request.TraitName,
                X = Array.CreateInstance(typeof(double), data.Count()),
                Y = Array.CreateInstance(typeof(int), data.Count()),
                XLabel = "Value",
                YLabel = "Depth"
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
