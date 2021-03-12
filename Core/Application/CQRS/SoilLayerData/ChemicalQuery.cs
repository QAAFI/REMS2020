using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Soils;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Chemical model for an experiment
    /// </summary>
    public class ChemicalQuery : IRequest<Chemical>, IParameterised
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }  

        public void Parameterise(params object[] args)
        {
            int count = GetType().GetProperties().Length;
            if (args.Length != count) 
                throw new Exception($"Invalid number of parameters. \n Expected: {count} \n Received: {args.Length}");

            ExperimentId = this.CastParam<int>(args[0]);
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
                Name = "Chemical",
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
