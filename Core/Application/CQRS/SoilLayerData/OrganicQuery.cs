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
    /// Generates and APSIM Organic model for an experiment
    /// </summary>
    public class OrganicQuery : IRequest<Organic>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class OrganicQueryHandler : IRequestHandler<OrganicQuery, Organic>
    {
        private readonly IRemsDbContext _context;

        public OrganicQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Organic> Handle(OrganicQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private Organic Handler(OrganicQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray();
            var thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray();
            var carbon = _context.GetSoilLayerTraitData(layers, "Carbon");
            var soilCNRatio = _context.GetSoilLayerTraitData(layers, "SoilCNRatio");
            var FBiom = _context.GetSoilLayerTraitData(layers, "FBiom");
            var FInert = _context.GetSoilLayerTraitData(layers, "FInert");
            var FOM = _context.GetSoilLayerTraitData(layers, "FOM");

            var organic = new Organic()
            {
                Name = "Organic",
                Depth = request.Report.ValidateItem(depth, "Organic.Depth"),
                Thickness = request.Report.ValidateItem(thickness, "Organic.Thickness"),
                Carbon = request.Report.ValidateItem(carbon, "Organic.Carbon"),
                SoilCNRatio = request.Report.ValidateItem(soilCNRatio, "Organic.SoilCNRatio"),
                FBiom = request.Report.ValidateItem(FBiom, "Organic.FBiom"),
                FInert = request.Report.ValidateItem(FInert, "Organic.FInert"),
                FOM = request.Report.ValidateItem(FOM, "Organic.FOM")
            };

            return organic;
        }
    }
}
