using System;
using System.Linq;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Chemical model for an experiment
    /// </summary>
    public class ChemicalQuery : ContextQuery<Chemical>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ChemicalQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Chemical Run()
        {
            var layers = _context.GetSoilLayers(ExperimentId);

            var depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth}").ToArray();
            var thickness = layers.Select(l => (double)(l.ToDepth - l.FromDepth)).ToArray();
            var NO3N = _context.GetSoilLayerTraitData(layers, nameof(Chemical.NO3N));
            var NH4N = _context.GetSoilLayerTraitData(layers, nameof(Chemical.NH4N));
            var PH = _context.GetSoilLayerTraitData(layers, nameof(Chemical.PH));

            bool valid = Report.ValidateItem(depth, nameof(Chemical.Depth))
                & Report.ValidateItem(thickness, nameof(Chemical.Thickness))
                & Report.ValidateItem(NO3N, nameof(Chemical.NO3N))
                & Report.ValidateItem(NH4N, nameof(Chemical.NH4N))
                & Report.ValidateItem(PH, nameof(Chemical.PH));

            Report.CommitValidation(nameof(Chemical), !valid);

            var chemical = new Chemical()
            {
                Name = "Chemical",
                Depth = depth,
                Thickness = thickness,
                NO3N = NO3N,
                NH4N = NH4N,
                PH = PH
            };

            return chemical;
        }
    }
}
