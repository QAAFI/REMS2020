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

        public SoilLayerDepthQueryHandler(IRemsDbFactory factory)
        {
            _context = factory.Context;
        }

        public async Task<string[]> Handle(SoilLayerDepthQuery request, CancellationToken cancellationToken)
        {
            return (from layer in _context.SoilLayers
                   where layer.SoilId == request.SoilId
                   orderby layer.FromDepth
                   select $"{layer.FromDepth ?? 0}-{layer.ToDepth ?? 0}").ToArray();
        }
    }
}