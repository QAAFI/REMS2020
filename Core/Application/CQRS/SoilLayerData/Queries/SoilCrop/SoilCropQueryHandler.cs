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
    public class SoilCropQueryHandler : IRequestHandler<SoilCropQuery, SoilCrop>
    {
        private readonly IRemsDbContext _context;

        public SoilCropQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SoilCrop> Handle(SoilCropQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private SoilCrop Handler(SoilCropQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var crop = new SoilCrop()
            {
                Name = "SorghumSoil",
                LL = _context.GetSoilLayerTraitData(layers, "LL"),
                KL = _context.GetSoilLayerTraitData(layers, "KL"),
                XF = _context.GetSoilLayerTraitData(layers, "XF")
            };

            return crop;
        }
    }
}