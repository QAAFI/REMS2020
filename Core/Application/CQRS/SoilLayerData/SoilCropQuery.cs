using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Soils;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM SoilCrop model for an experiment
    /// </summary>
    public class SoilCropQuery : IRequest<SoilCrop>, IParameterised
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

    public class SoilCropQueryHandler : IRequestHandler<SoilCropQuery, SoilCrop>
    {
        private readonly IRemsDbContext _context;

        public SoilCropQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SoilCrop> Handle(SoilCropQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private SoilCrop Handler(SoilCropQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var crop = new SoilCrop()
            {
                Name = "SorghumSoil",
                LL = _context.GetSoilLayerTraitData(layers, "LL"),
                KL = _context.GetSoilLayerTraitData(layers, "KL"),
                XF = _context.GetSoilLayerTraitData(layers, "XF")
            };

            return crop;
        }
    }
}
