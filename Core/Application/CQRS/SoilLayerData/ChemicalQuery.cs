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
    public class ChemicalQuery : IRequest<Chemical>, IParameterised
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

    public class ChemicalQueryHandler : IRequestHandler<ChemicalQuery, Chemical>
    {
        private readonly IRemsDbContext _context;

        public ChemicalQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Chemical> Handle(ChemicalQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private Chemical Handler(ChemicalQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var chemical = new Chemical()
            {
                Name = "Organic",
                Depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray(),
                Thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray(),
                NO3N = _context.GetSoilLayerTraitData(layers, "NO3N"),
                NH4N = _context.GetSoilLayerTraitData(layers, "NH4N"),
                PH = _context.GetSoilLayerTraitData(layers, "PH")
            };

            return chemical;
        }
    }
}
