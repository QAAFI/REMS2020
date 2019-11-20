using MediatR;

using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Rems.Application.Common.Interfaces;

namespace Rems.Application.Soils.Queries.x
{
    public class SoilLayerDepthQueryHandler : IRequestHandler<SoilLayerDepthQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public SoilLayerDepthQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public async Task<string[]> Handle(SoilLayerDepthQuery request, CancellationToken cancellationToken)
        {
            return (from layer in _context.SoilLayers
                   where layer.SoilId == request.SoilId
                   orderby layer.DepthFrom
                   select $"({layer.DepthFrom ?? 0}-{layer.DepthTo ?? 0}").ToArray();
        }
    }
}