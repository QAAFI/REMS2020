using System;
using System.Linq;

using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Physical model for an experiment
    /// </summary>
    public class PhysicalQuery :ContextQuery<Physical>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<PhysicalQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Physical Run()
        {
            var layers = _context.GetSoilLayers(ExperimentId);

            var thickness = layers.Select(l => (double)(l.ToDepth - l.FromDepth)).ToArray();
            var bd = _context.GetSoilLayerTraitData(layers, nameof(Physical.BD));
            var airdry = _context.GetSoilLayerTraitData(layers, nameof(Physical.AirDry));
            var ll15 = _context.GetSoilLayerTraitData(layers, nameof(Physical.LL15));
            var dul = _context.GetSoilLayerTraitData(layers, nameof(Physical.DUL));
            var sat = _context.GetSoilLayerTraitData(layers, nameof(Physical.SAT));
            var ks = _context.GetSoilLayerTraitData(layers, nameof(Physical.KS));

            bool valid = Report.ValidateItem(thickness, nameof(Physical.Thickness))
                & Report.ValidateItem(bd, nameof(Physical.BD))
                & Report.ValidateItem(airdry, nameof(Physical.AirDry))
                & Report.ValidateItem(ll15, nameof(Physical.LL15))
                & Report.ValidateItem(dul, nameof(Physical.DUL))
                & Report.ValidateItem(sat, nameof(Physical.SAT))
                & Report.ValidateItem(ks, nameof(Physical.KS));

            Report.CommitValidation(nameof(Physical), !valid);

            var physical = new Physical()
            {
                Name = nameof(Physical),
                Thickness = thickness,
                BD = bd,
                AirDry = airdry,
                LL15 = ll15,
                DUL = dul,
                SAT = sat,
                KS = ks
            };

            return physical;
        }
    }
}
