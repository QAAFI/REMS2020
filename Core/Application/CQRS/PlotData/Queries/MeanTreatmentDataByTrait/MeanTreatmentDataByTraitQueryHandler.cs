using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Domain.Entities;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using System.Linq;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class MeanTreatmentDataByTraitQueryHandler : IRequestHandler<MeanTreatmentDataByTraitQuery, SeriesData>
    {
        private readonly IRemsDbFactory factory;

        public MeanTreatmentDataByTraitQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<SeriesData> Handle(MeanTreatmentDataByTraitQuery request, CancellationToken token)
        {
            var data = factory.Context.PlotData
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
                XLabel = "Date",
                YLabel = "Value"
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
