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
            var swcon = _context.GetSoilLayerTraitData(layers, "SWCON");
            var klat = _context.GetSoilLayerTraitData(layers, "KLAT");

            var water = new WaterBalance()
            {
                Name = "SoilWater",
                Thickness = request.Report.ValidateItem(thickness, "WaterBalance.Thickness"),
                SWCON = request.Report.ValidateItem(swcon, "WaterBalance.SWCON"),
                KLAT = request.Report.ValidateItem(klat, "WaterBalance.KLAT"),
                SummerDate = "1-Nov",
                SummerU = 6.0,
                SummerCona = 3.5,
                WinterDate = "1-Apr",
                WinterU = 6.0,
                WinterCona = 3.5,
                Salb = 0.11
            };

            return water;
        }
    }
}
