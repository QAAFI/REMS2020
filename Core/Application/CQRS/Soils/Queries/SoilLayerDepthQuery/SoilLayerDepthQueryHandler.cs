using MediatR;

using Rems.Application.Common.Interfaces;

using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.Soils.Queries.x
{
    public class SoilLayerDepthQueryHandler : IRequestHandler<SoilLayerDepthQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public SoilLayerDepthQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(SoilLayerDepthQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _context.SoilLayers
                .Where(l => l.SoilId == request.SoilId)
                .OrderBy(l => l.FromDepth)
                .Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}")
                .ToArray()
            );
        }
    }
}