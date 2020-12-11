using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class SoilLayerDatesQuery : IRequest<DateTime[]>
    {
        public int TreatmentId { get; set; }
    }

    public class SoilLayerDatesQueryHandler : IRequestHandler<SoilLayerDatesQuery, DateTime[]>
    {
        private readonly IRemsDbContext _context;

        public SoilLayerDatesQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<DateTime[]> Handle(SoilLayerDatesQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private DateTime[] Handler(SoilLayerDatesQuery request, CancellationToken token)
        {
            return _context.SoilLayerDatas
                .Where(d => d.Plot.TreatmentId == request.TreatmentId)
                .Select(d => d.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToArray();
        }
    }
}
