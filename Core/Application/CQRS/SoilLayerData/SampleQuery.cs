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
    public class SampleQuery : IRequest<Sample>, IParameterised
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

    public class SampleQueryHandler : IRequestHandler<SampleQuery, Sample>
    {
        private readonly IRemsDbContext _context;

        public SampleQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Sample> Handle(SampleQuery request, CancellationToken token) => Task.Run(() => Handler(request));

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
