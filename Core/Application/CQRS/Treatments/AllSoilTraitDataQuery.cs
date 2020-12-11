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
    public class AllSoilTraitDataQuery : IRequest<IEnumerable<SeriesData>>
    {
        public int TreatmentId { get; set; }

        public string TraitName { get; set; }

        public DateTime Date { get; set; }
    }

    public class AllSoilTraitDataQueryHandler : IRequestHandler<AllSoilTraitDataQuery, IEnumerable<SeriesData>>
    {
        private readonly IRemsDbContext _context;

        public AllSoilTraitDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<SeriesData>> Handle(AllSoilTraitDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private IEnumerable<SeriesData> Handler(AllSoilTraitDataQuery request, CancellationToken token)
        {
            var plots = _context.Treatments.Find(request.TreatmentId).Plots;

            foreach (var plot in plots)
                yield return GetPlotData(plot.PlotId, request.Date, request.TraitName);
        }

        private SeriesData GetPlotData(int id, DateTime date, string trait)
        {
            var data = _context.SoilLayerDatas
                .Where(d => d.PlotId == id)
                .Where(d => d.Date == date)
                .Where(d => d.Trait.Name == trait)
                .OrderBy(d => d.DepthFrom)
                .ToArray();

            var plot = _context.Plots.Find(id);
            var x = plot.Repetition.ToString();
            string name = x + " " + trait + ", " + date.ToString("dd/MM/yy");

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
