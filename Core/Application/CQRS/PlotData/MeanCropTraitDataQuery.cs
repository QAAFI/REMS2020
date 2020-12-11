using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class MeanCropTraitDataQuery : IRequest<SeriesData>
    {
        public int TreatmentId { get; set; }

        public string TraitName { get; set; }
    }

    public class MeanCropTraitDataQueryHandler : IRequestHandler<MeanCropTraitDataQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public MeanCropTraitDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(MeanCropTraitDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(MeanCropTraitDataQuery request, CancellationToken token)
        {
            var data = _context.PlotData
                .Where(p => p.Plot.TreatmentId == request.TreatmentId)
                .Where(p => p.Trait.Name == request.TraitName)
                .ToArray() // Have to cast to an array to support the following GroupBy
                .GroupBy(p => p.Date)
                .OrderBy(g => g.Key)
                .ToArray();

            SeriesData series = new SeriesData()
            {
                Name = request.TraitName,
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
