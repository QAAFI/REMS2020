using MediatR;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerThicknessQueryHandler : IRequestHandler<SoilLayerThicknessQuery, IEnumerable<double>>
    {
        private readonly IRemsDbContext _context;

        public SoilLayerThicknessQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<double>> Handle(SoilLayerThicknessQuery request, CancellationToken cancellationToken)
        {
            return from layer in _context.SoilLayers
                   where layer.SoilId == request.SoilId
                   select (double)((layer.DepthTo ?? 0) - (layer.DepthFrom ?? 0));
        }
    }
}