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
    public class SampleQuery : IRequest<Sample>, IParameterised
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

    public class SampleQueryHandler : IRequestHandler<SampleQuery, Sample>
    {
        private readonly IRemsDbContext _context;

        public SampleQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Sample> Handle(SampleQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Sample Handler(SampleQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var sample = new Sample()
            {
                Name = "Water",
                Depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray(),
                Thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray(),
                SW = _context.GetSoilLayerTraitData(layers, "SW")
            };

            return sample;
        }
    }
}
