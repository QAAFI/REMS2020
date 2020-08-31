using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;
using Models.Soils;
using Rems.Application.Common.Extensions;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class ChemicalQueryHandler : IRequestHandler<ChemicalQuery, Chemical>
    {
        private readonly IRemsDbContext _context;

        public ChemicalQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Chemical> Handle(ChemicalQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Chemical Handler(ChemicalQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var chemical = new Chemical()
            {
                Name = "Organic",
                Depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray(),
                Thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray(),
                NO3N = _context.GetSoilLayerTraitData(layers, "NO3N"),
                NH4N = _context.GetSoilLayerTraitData(layers, "NH4N"),
                PH = _context.GetSoilLayerTraitData(layers, "PH")
            };

            return chemical;
        }
    }
}