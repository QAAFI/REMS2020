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
    public class SoilLayerThicknessQuery : IRequest<IEnumerable<double>>
    {
        public IRemsDbContext Context { get; set; }

        public Soil Soil { get; set; }

        public class Handler : IRequestHandler<SoilLayerThicknessQuery, IEnumerable<double>>
        {
            public Handler()
            { }

            public async Task<IEnumerable<double>> Handle(SoilLayerThicknessQuery request, CancellationToken cancellationToken)
            {
                return from layer in request.Context.SoilLayers
                        where layer.Soil == request.Soil
                        select (double)((layer.DepthTo ?? 0) - (layer.DepthFrom ?? 0));
            }
        }
    }
}