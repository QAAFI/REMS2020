using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Chemical model for an experiment
    /// </summary>
    public class ChemicalQuery : IRequest<Chemical>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
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

            var depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray();
            var thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray();
            var NO3N = _context.GetSoilLayerTraitData(layers, nameof(Chemical.NO3N));
            var NH4N = _context.GetSoilLayerTraitData(layers, nameof(Chemical.NH4N));
            var PH = _context.GetSoilLayerTraitData(layers, nameof(Chemical.PH));

            bool valid = request.Report.ValidateItem(depth, nameof(Chemical.Depth))
                & request.Report.ValidateItem(thickness, nameof(Chemical.Thickness))
                & request.Report.ValidateItem(NO3N, nameof(Chemical.NO3N))
                & request.Report.ValidateItem(NH4N, nameof(Chemical.NH4N))
                & request.Report.ValidateItem(PH, nameof(Chemical.PH));

            request.Report.CommitValidation(nameof(Chemical), !valid);

            var chemical = new Chemical()
            {
                Name = "Chemical",
                Depth = depth,
                Thickness = thickness,
                NO3N = NO3N,
                NH4N = NH4N,
                PH = PH
            };

            return chemical;
        }
    }
}
