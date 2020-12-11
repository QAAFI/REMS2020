using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class MeanSoilTraitDataQuery : IRequest<SeriesData>
    {
        public int TreatmentId { get; set; }

        public string TraitName { get; set; }

        public DateTime Date { get; set; }
    }

    public class MeanSoilTraitDataQueryHandler : IRequestHandler<MeanSoilTraitDataQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public MeanSoilTraitDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(MeanSoilTraitDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(MeanSoilTraitDataQuery request, CancellationToken token)
        {
            var data = _context.SoilLayerDatas
                .Where(p => p.Plot.TreatmentId == request.TreatmentId)
                .Where(p => p.Trait.Name == request.TraitName)                
                .Where(p => p.Date == request.Date)
                .ToArray()
                .GroupBy(p => p.DepthFrom)
                .OrderBy(g => g.Key)
                .ToArray();

            string name = request.TraitName + ", " + request.Date.ToString("dd/MM/yy");

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
                var group = data[i];
                var value = group.Average(p => p.Value);

                series.X.SetValue(value, i);
                series.Y.SetValue(group.Key, i);
            }

            return series;
        }
    }
}
