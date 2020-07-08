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

        public SoilLayerThicknessQueryHandler(IRemsDbFactory factory)
        {
            _context = factory.Context;
        }

        public async Task<double[]> Handle(SoilLayerThicknessQuery request, CancellationToken cancellationToken)
        {
            return (from layer in _context.SoilLayers
                   where layer.SoilId == request.SoilId
                   orderby layer.FromDepth
                   select (double)((layer.ToDepth ?? 0) - (layer.FromDepth ?? 0))).ToArray();
        }
    }
}