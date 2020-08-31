using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;
using Models.Soils;
using Rems.Application.Common.Extensions;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PhysicalQueryHandler : IRequestHandler<PhysicalQuery, Physical>
    {
        private readonly IRemsDbContext _context;

        public PhysicalQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Physical> Handle(PhysicalQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Physical Handler(PhysicalQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray();

            var physical = new Physical()
            {
                Name = "Physical",
                Thickness = thickness,
                BD = _context.GetSoilLayerTraitData(layers, "BD"),
                AirDry = _context.GetSoilLayerTraitData(layers, "AirDry"),
                LL15 = _context.GetSoilLayerTraitData(layers, "LL15"),
                DUL = _context.GetSoilLayerTraitData(layers, "DUL"),
                SAT = _context.GetSoilLayerTraitData(layers,  "SAT"),
                KS = _context.GetSoilLayerTraitData(layers, "KS")
            };

            return physical;
        }
    }
}