using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using System.Collections.Generic;

namespace Rems.Application.CQRS
{
    public class AllCropTraitDataQuery : IRequest<IEnumerable<SeriesData>>
    {
        public int TreatmentId { get; set; }

        public string TraitName { get; set; }
    }

    public class AllDataByTraitQueryHandler : IRequestHandler<AllCropTraitDataQuery, IEnumerable<SeriesData>>
    {
        private readonly IRemsDbContext _context;

        public AllDataByTraitQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<SeriesData>> Handle(AllCropTraitDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private IEnumerable<SeriesData> Handler(AllCropTraitDataQuery request, CancellationToken token)
        {
            var plots = _context.Treatments.Find(request.TreatmentId).Plots;

            foreach (var plot in plots)
                yield return GetPlotData(plot.PlotId, request.TraitName);
        }

        private SeriesData GetPlotData(int id, string trait)
        {
            var data = _context.PlotData
                .Where(p => p.Plot.PlotId == id)
                .Where(p => p.Trait.Name == trait)
                .OrderBy(p => p.Date)
                .ToArray();

            if (data.Length == 0) return null;

            var rep = _context.Plots.Where(p => p.PlotId == id);
            var x = rep.Select(p => p.Repetition).First();
            string name = trait + " " + x;

            SeriesData series = new SeriesData()
            {
                Name = name,
                X = Array.CreateInstance(typeof(DateTime), data.Count()),
                Y = Array.CreateInstance(typeof(double), data.Count()),
                XName = "Value",
                YName = "Date"
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
