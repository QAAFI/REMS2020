using System;
using System.Linq;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates and APSIM Organic model for an experiment
    /// </summary>
    public class OrganicQuery : ContextQuery<Organic>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<OrganicQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Organic Run()
        {
            // Find the values
            var layers = _context.GetSoilLayers(ExperimentId);

            var depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth}").ToArray();
            var thickness = layers.Select(l => (double)(l.ToDepth - l.FromDepth)).ToArray();
            var carbon = _context.GetSoilLayerTraitData(layers, nameof(Organic.Carbon));
            var soilCNRatio = _context.GetSoilLayerTraitData(layers, nameof(Organic.SoilCNRatio));
            var FBiom = _context.GetSoilLayerTraitData(layers, nameof(Organic.FBiom));
            var FInert = _context.GetSoilLayerTraitData(layers, nameof(Organic.FInert));
            var FOM = _context.GetSoilLayerTraitData(layers, nameof(Organic.FOM));

            // Report on the validity of the values
            bool valid =
                Report.ValidateItem(depth, nameof(Organic.Depth))
                & Report.ValidateItem(thickness, nameof(Organic.Thickness))
                & Report.ValidateItem(carbon, nameof(Organic.Carbon))
                & Report.ValidateItem(soilCNRatio, nameof(Organic.SoilCNRatio))
                & Report.ValidateItem(FBiom, nameof(Organic.FBiom))
                & Report.ValidateItem(FInert, nameof(Organic.FInert))
                & Report.ValidateItem(FOM, nameof(Organic.FOM));

            Report.CommitValidation(nameof(Organic), !valid);

            // Create the model
            var organic = new Organic()
            {
                Name = nameof(Organic),
                Depth = depth,
                Thickness = thickness,
                Carbon = carbon,
                SoilCNRatio = soilCNRatio,
                FBiom = FBiom,
                FInert = FInert,
                FOM = FOM
            };

            return organic;
        }
    }
}
