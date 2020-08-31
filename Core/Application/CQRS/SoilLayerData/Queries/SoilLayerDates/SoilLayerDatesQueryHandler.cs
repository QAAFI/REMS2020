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
        private readonly IRemsDbContext _context;

        public SoilLayerDatesQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<DateTime[]> Handle(SoilLayerDatesQuery request, CancellationToken token)
        {
            return Task.Run(() =>_context.SoilLayerDatas
                .Where(d => d.Plot.TreatmentId == request.TreatmentId)
                .Select(d => d.Date)
                .Distinct()
                .ToArray()
            );
        }
    }
}