using MediatR;

using Rems.Application.Common.Interfaces;

using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerDataQueryHandler : IRequestHandler<SoilLayerDataQuery, double[]>
    {
        private readonly IRemsDbContext _context;

        public SoilLayerDataQueryHandler(IRemsDbFactory factory)
        {
            _context = factory.Context;
        }

        public async Task<double[]> Handle(SoilLayerDataQuery request, CancellationToken cancellationToken)
        {
            return (from data in _context.SoilLayerDatas
                   where data.PlotId == request.PlotId
                   where data.Trait.Name == request.TraitName
                   orderby data.DepthFrom
                   select data.Value ?? 0.0).ToArray();
        }
    }
}