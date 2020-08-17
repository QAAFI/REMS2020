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
    public class FertilizationDataQueryHandler : IRequestHandler<FertilizationDataQuery, SeriesData>
    {
        private readonly IRemsDbFactory factory;

        public FertilizationDataQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<SeriesData> Handle(FertilizationDataQuery request, CancellationToken token)
        {
            var fertilizations = factory.Context.Fertilizations
                .Where(i => i.TreatmentId == request.TreatmentId)                
                .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), fertilizations.Count()),
                Y = Array.CreateInstance(typeof(double), fertilizations.Count()),
                XLabel = "Date",
                YLabel = "Amount"
            };

            for (int i = 0; i < fertilizations.Length; i++)
            {
                var item = fertilizations[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Amount, i);
            }

            return data;
        }
    }
}