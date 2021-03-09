using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.WaterModel;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common;

namespace Rems.Application.CQRS
{
    public class WaterBalanceQuery : IRequest<WaterBalance>, IParameterised
    {
        public int ExperimentId { get; set; }

        public void Parameterise(params object[] args)
        {
            int count = GetType().GetProperties().Length;
            if (args.Length != count)
                throw new Exception($"Invalid number of parameters. \n Expected: {count} \n Received: {args.Length}");

            ExperimentId = this.CastParam<int>(args[0]);
        }
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

            var water = new WaterBalance()
            {
                Name = "SoilWater",
                Thickness = thickness,
                SWCON = _context.GetSoilLayerTraitData(layers, "SWCON"),
                KLAT = _context.GetSoilLayerTraitData(layers, "KLAT"),
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
