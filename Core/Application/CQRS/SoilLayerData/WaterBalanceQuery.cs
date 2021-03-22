using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.WaterModel;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM WaterBalance model for an experiment
    /// </summary>
    public class WaterBalanceQuery : IRequest<WaterBalance>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class WaterBalanceQueryHandler : IRequestHandler<WaterBalanceQuery, WaterBalance>
    {
        private readonly IRemsDbContext _context;

        public WaterBalanceQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<WaterBalance> Handle(WaterBalanceQuery request, CancellationToken token) =>  Task.Run(() => Handler(request));

        private WaterBalance Handler(WaterBalanceQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray();
            var swcon = _context.GetSoilLayerTraitData(layers, nameof(WaterBalance.SWCON));
            var klat = _context.GetSoilLayerTraitData(layers, nameof(WaterBalance.KLAT));

            bool valid = 
                request.Report.ValidateItem(thickness, nameof(WaterBalance.Thickness))
                & request.Report.ValidateItem(swcon, nameof(WaterBalance.SWCON))
                & request.Report.ValidateItem(klat, nameof(WaterBalance.KLAT));

            request.Report.CommitValidation(nameof(WaterBalance), !valid);

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
                CatchmentArea = double.NaN
            };

            return water;
        }
    }
}
