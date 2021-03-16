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
    /// Generates an APSIM Physical model for an experiment
    /// </summary>
    public class PhysicalQuery : IRequest<Physical>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class PhysicalQueryHandler : IRequestHandler<PhysicalQuery, Physical>
    {
        private readonly IRemsDbContext _context;

        public PhysicalQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Physical> Handle(PhysicalQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private Physical Handler(PhysicalQuery request)
        {
            var layers = _context.GetSoilLayers(request.ExperimentId);

            var thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray();

            var bd = _context.GetSoilLayerTraitData(layers, "BD");
            var airdry = _context.GetSoilLayerTraitData(layers, "AirDry");
            var ll15 = _context.GetSoilLayerTraitData(layers, "LL15");
            var dul = _context.GetSoilLayerTraitData(layers, "DUL");
            var sat = _context.GetSoilLayerTraitData(layers, "SAT");
            var ks = _context.GetSoilLayerTraitData(layers, "KS");

            var physical = new Physical()
            {
                Name = "Physical",
                Thickness = thickness,
                BD = request.Report.ValidateItem(bd, "Physical.BD"),
                AirDry = request.Report.ValidateItem(airdry, "Physical.AirDry"),
                LL15 = request.Report.ValidateItem(ll15, "Physical.LL15"),
                DUL = request.Report.ValidateItem(dul, "Physical.DUL"),
                SAT = request.Report.ValidateItem(sat, "Physical.SAT"),
                KS = request.Report.ValidateItem(ks, "Physical.KS")
            };

            return physical;
        }
    }
}
