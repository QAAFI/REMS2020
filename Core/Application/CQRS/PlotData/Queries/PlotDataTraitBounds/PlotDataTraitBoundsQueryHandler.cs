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
        private readonly IRemsDbContext _context;

        public PlotDataTraitBoundsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<PlotDataBounds> Handle(PlotDataTraitBoundsQuery request, CancellationToken token)
        {
            return Task.Run(() =>
            {
                var data = _context.PlotData
                .Where(p => p.Trait.Name == request.TraitName);

                return new PlotDataBounds()
                {
                    YMin = data.Min(p => p.Value),
                    YMax = data.Max(p => p.Value),
                    XMin = data.Min(p => p.Date),
                    XMax = data.Max(p => p.Date)
                };
            });
        }
    }
}
