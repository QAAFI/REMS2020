using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class SoilLayerDatesQueryHandler : IRequestHandler<SoilLayerDatesQuery, DateTime[]>
    {
        private readonly IRemsDbFactory factory;

        public SoilLayerDatesQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<DateTime[]> Handle(SoilLayerDatesQuery request, CancellationToken token)
        {
            var dates = factory.Context.SoilLayerDatas
                .Where(d => d.Plot.TreatmentId == request.TreatmentId)
                .Select(d => d.Date)
                .Distinct()
                .ToArray();

            return dates;
        }
    }
}