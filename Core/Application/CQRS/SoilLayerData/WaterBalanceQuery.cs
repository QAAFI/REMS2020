using System;
using System.Linq;
using Models.WaterModel;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM WaterBalance model for an experiment
    /// </summary>
    public class WaterBalanceQuery : ContextQuery<WaterBalance>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<WaterBalanceQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override WaterBalance Run()
        {
            var layers = _context.GetSoilLayers(ExperimentId);

            var thickness = layers.Select(l => (double)(l.ToDepth - l.FromDepth)).ToArray();
            var swcon = _context.GetSoilLayerTraitData(layers, nameof(WaterBalance.SWCON));
            var klat = _context.GetSoilLayerTraitData(layers, nameof(WaterBalance.KLAT));

            bool valid = 
                Report.ValidateItem(thickness, nameof(WaterBalance.Thickness))
                & Report.ValidateItem(swcon, nameof(WaterBalance.SWCON))
                & Report.ValidateItem(klat, nameof(WaterBalance.KLAT));

            Report.CommitValidation(nameof(WaterBalance), !valid);

            var water = new WaterBalance()
            {
                Name = "SoilWater",
                Thickness = thickness,
                SWCON = swcon,
                KLAT = klat,
                SummerDate = "1-Nov",
                SummerU = 1.5,
                SummerCona = 6.5,
                WinterDate = "1-Apr",
                WinterU = 1.5,
                WinterCona = 6.5,
                DiffusConst = 40,
                DiffusSlope = 16,
                Salb = 0.2,
                CN2Bare = 85,
                DischargeWidth = double.NaN,
                CatchmentArea = double.NaN,
                ResourceName = "WaterBalance"
            };

            return water;
        }
    }
}
