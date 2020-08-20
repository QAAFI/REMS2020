using MediatR;

using Rems.Application.Common.Interfaces;

using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerThicknessQueryHandler : IRequestHandler<SoilLayerThicknessQuery, double[]>
    {
        private readonly IRemsDbContext _context;

        public SoilLayerThicknessQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<double[]> Handle(SoilLayerThicknessQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _context.SoilLayers
                .Where(l => l.SoilId == request.SoilId)
                .OrderBy(l => l.FromDepth)
                .Select(layer => (double)((layer.ToDepth ?? 0) - (layer.FromDepth ?? 0)))
                .ToArray()
            );
        }
    }
}