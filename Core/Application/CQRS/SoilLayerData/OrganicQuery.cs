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
            // Find the values
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray();
            var thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray();
            var carbon = _context.GetSoilLayerTraitData(layers, nameof(Organic.Carbon));
            var soilCNRatio = _context.GetSoilLayerTraitData(layers, nameof(Organic.SoilCNRatio));
            var FBiom = _context.GetSoilLayerTraitData(layers, nameof(Organic.FBiom));
            var FInert = _context.GetSoilLayerTraitData(layers, nameof(Organic.FInert));
            var FOM = _context.GetSoilLayerTraitData(layers, nameof(Organic.FOM));

            // Report on the validity of the values
            bool valid =
                request.Report.ValidateItem(depth, nameof(Organic.Depth))
                & request.Report.ValidateItem(thickness, nameof(Organic.Thickness))
                & request.Report.ValidateItem(carbon, nameof(Organic.Carbon))
                & request.Report.ValidateItem(soilCNRatio, nameof(Organic.SoilCNRatio))
                & request.Report.ValidateItem(FBiom, nameof(Organic.FBiom))
                & request.Report.ValidateItem(FInert, nameof(Organic.FInert))
                & request.Report.ValidateItem(FOM, nameof(Organic.FOM));

            request.Report.CommitValidation(nameof(Organic), !valid);

            // Create the model
            var organic = new Organic()
            {
                Name = nameof(Organic),
                Depth = depth,
                Thickness = thickness,
                Carbon = carbon,
                SoilCNRatio = soilCNRatio,
                FBiom = FBiom,
                FInert = FInert,
                FOM = FOM
            };

            return organic;
        }
    }
}
