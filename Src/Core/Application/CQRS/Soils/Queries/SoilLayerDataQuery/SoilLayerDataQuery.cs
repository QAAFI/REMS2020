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
    public class SoilLayerDataQuery : IRequest<IEnumerable<double>>
    {
        public IRemsDbContext Context { get; set; }

        public Plot Plot { get; set; }

        public Trait Trait { get; set; }

        public class Handler : IRequestHandler<SoilLayerDataQuery, IEnumerable<double>>
        {
            public Handler()
            { }

            public async Task<IEnumerable<double>> Handle(SoilLayerDataQuery request, CancellationToken cancellationToken)
            {
                return from data in request.Context.SoilLayerDatas
                        where data.Plot == request.Plot
                        where data.Trait == request.Trait
                        orderby data.DepthFrom
                        select data.Value ?? 0.0;
            }
        }
    }
}