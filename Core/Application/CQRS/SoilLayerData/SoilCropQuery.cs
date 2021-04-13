using System;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM SoilCrop model for an experiment
    /// </summary>
    public class SoilCropQuery : ContextQuery<SoilCrop>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilCropQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SoilCrop Run()
        {
            var layers = _context.GetSoilLayers(ExperimentId);

            var ll = _context.GetSoilLayerTraitData(layers, nameof(SoilCrop.LL));
            var kl = _context.GetSoilLayerTraitData(layers, nameof(SoilCrop.KL));
            var xf = _context.GetSoilLayerTraitData(layers, nameof(SoilCrop.XF));

            bool valid = Report.ValidateItem(ll, nameof(SoilCrop.LL))
                & Report.ValidateItem(kl, nameof(SoilCrop.KL))
                & Report.ValidateItem(xf, nameof(SoilCrop.XF));

            Report.CommitValidation(nameof(SoilCrop), !valid);

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
