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
    public class TillagesDataQueryHandler : IRequestHandler<TillagesDataQuery, SeriesData>
    {
        private readonly IRemsDbFactory factory;

        public TillagesDataQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<SeriesData> Handle(TillagesDataQuery request, CancellationToken token)
        {
            var tillages = factory.Context.Tillages
                .Where(i => i.TreatmentId == request.TreatmentId)                
                .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), tillages.Count()),
                Y = Array.CreateInstance(typeof(double), tillages.Count()),
                XLabel = "Date",
                YLabel = "Depth"
            };

            for (int i = 0; i < tillages.Length; i++)
            {
                var item = tillages[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Depth, i);
            }

            return data;
        }
    }
}