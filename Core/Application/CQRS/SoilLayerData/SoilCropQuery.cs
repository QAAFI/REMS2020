using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM SoilCrop model for an experiment
    /// </summary>
    public class SoilCropQuery : IRequest<SoilCrop>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class SoilCropQueryHandler : IRequestHandler<SoilCropQuery, SoilCrop>
    {
        private readonly IRemsDbContext _context;

        public SoilCropQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SoilCrop> Handle(SoilCropQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private SoilCrop Handler(SoilCropQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var ll = _context.GetSoilLayerTraitData(layers, "LL");
            var kl = _context.GetSoilLayerTraitData(layers, "KL");
            var xf = _context.GetSoilLayerTraitData(layers, "XF");

            var crop = new SoilCrop()
            {
                Name = "SorghumSoil",
                LL = request.Report.ValidateItem(ll, "SoilCrop.LL"),
                KL = request.Report.ValidateItem(kl, "SoilCrop.KL"),
                XF = request.Report.ValidateItem(xf, "SoilCrop.XF")
            };

            return crop;
        }
    }
}
