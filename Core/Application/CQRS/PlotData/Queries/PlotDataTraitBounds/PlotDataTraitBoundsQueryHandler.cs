using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;
using Rems.Application.Common;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlotDataTraitBoundsQueryHandler : IRequestHandler<PlotDataTraitBoundsQuery, PlotDataBounds>
    {
        private readonly IRemsDbFactory factory;

        public PlotDataTraitBoundsQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<PlotDataBounds> Handle(PlotDataTraitBoundsQuery request, CancellationToken token)
        {
            var data = factory.Context.PlotData
                .Where(p => p.Trait.Name == request.TraitName);

            return new PlotDataBounds()
            {
                YMin = data.Min(p => p.Value),
                YMax = data.Max(p => p.Value),
                XMin = data.Min(p => p.Date),
                XMax = data.Max(p => p.Date)
            };
        }
    }
}
