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
    public class IrrigationDataQueryHandler : IRequestHandler<IrrigationDataQuery, SeriesData>
    {
        private readonly IRemsDbFactory factory;

        public IrrigationDataQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<SeriesData> Handle(IrrigationDataQuery request, CancellationToken token)
        {
            var irrigations = factory.Context.Irrigations
                .Where(i => i.TreatmentId == request.TreatmentId)                
                .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), irrigations.Count()),
                Y = Array.CreateInstance(typeof(double), irrigations.Count()),
                XLabel = "Date",
                YLabel = "Amount"
            };

            for (int i = 0; i < irrigations.Length; i++)
            {
                var item = irrigations[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Amount, i);
            }

            return data;
        }
    }
}