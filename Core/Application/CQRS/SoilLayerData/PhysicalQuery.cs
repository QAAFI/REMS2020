using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Soils;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class PhysicalQuery : IRequest<Physical>, IParameterised
    {   
        public int ExperimentId { get; set; }

        public void Parameterise(params object[] args)
        {
            if (args.Length != 1) 
                throw new Exception($"Invalid number of parameters. \n Expected: 1 \n Received: {args.Length}");

            if (args[0] is int id)
                ExperimentId = id;
            else
                throw new Exception($"Invalid parameter type. \n Expected: {typeof(int)} \n Received: {args[0].GetType()}");
        }
    }

    public class PhysicalQueryHandler : IRequestHandler<PhysicalQuery, Physical>
    {
        private readonly IRemsDbContext _context;

        public PhysicalQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Physical> Handle(PhysicalQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Physical Handler(PhysicalQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray();

            var physical = new Physical()
            {
                Name = "Physical",
                Thickness = thickness,
                BD = _context.GetSoilLayerTraitData(layers, "BD"),
                AirDry = _context.GetSoilLayerTraitData(layers, "AirDry"),
                LL15 = _context.GetSoilLayerTraitData(layers, "LL15"),
                DUL = _context.GetSoilLayerTraitData(layers, "DUL"),
                SAT = _context.GetSoilLayerTraitData(layers, "SAT"),
                KS = _context.GetSoilLayerTraitData(layers, "KS")
            };

            return physical;
        }
    }
}
