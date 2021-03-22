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

            var ll = _context.GetSoilLayerTraitData(layers, nameof(SoilCrop.LL));
            var kl = _context.GetSoilLayerTraitData(layers, nameof(SoilCrop.KL));
            var xf = _context.GetSoilLayerTraitData(layers, nameof(SoilCrop.XF));

            bool valid = request.Report.ValidateItem(ll, nameof(SoilCrop.LL))
                & request.Report.ValidateItem(kl, nameof(SoilCrop.KL))
                & request.Report.ValidateItem(xf, nameof(SoilCrop.XF));

            request.Report.CommitValidation(nameof(SoilCrop), !valid);

            var crop = new SoilCrop()
            {
                Name = "SorghumSoil",
                LL = ll,
                KL = kl,
                XF = xf
            };

            return crop;
        }
    }
}
