using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;
using Models.Soils;
using Rems.Application.Common.Extensions;
using Models.WaterModel;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class OrganicQueryHandler : IRequestHandler<OrganicQuery, Organic>
    {
        private readonly IRemsDbContext _context;

        public OrganicQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Organic> Handle(OrganicQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Organic Handler(OrganicQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var organic = new Organic()
            {
                Name = "Water",
                Depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray(),
                Thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray(),
                Carbon = _context.GetSoilLayerTraitData(layers, "Carbon"),
                SoilCNRatio = _context.GetSoilLayerTraitData(layers, "SoilCNRatio"),
                FBiom = _context.GetSoilLayerTraitData(layers,"FBiom"),
                FInert = _context.GetSoilLayerTraitData(layers, "FInert"),
                FOM = _context.GetSoilLayerTraitData(layers, "FOM")
            };

            return organic;
        }
    }
}