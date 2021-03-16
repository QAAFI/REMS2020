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
            var NO3N = _context.GetSoilLayerTraitData(layers, "NO3N");
            var NH4N = _context.GetSoilLayerTraitData(layers, "NH4N");
            var PH = _context.GetSoilLayerTraitData(layers, "PH");

            var chemical = new Chemical()
            {
                Name = "Chemical",
                Depth = request.Report.ValidateItem(depth, "Chemical.Depth"),
                Thickness = request.Report.ValidateItem(thickness, "Chemical.Thickness"),
                NO3N = request.Report.ValidateItem(NO3N, "Chemical.NO3N"),
                NH4N = request.Report.ValidateItem(NH4N, "Chemical.NH4N"),
                PH = request.Report.ValidateItem(PH, "Chemical.PH")
            };

            return chemical;
        }
    }
}
