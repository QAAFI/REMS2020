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
            var bd = _context.GetSoilLayerTraitData(layers, nameof(Physical.BD));
            var airdry = _context.GetSoilLayerTraitData(layers, nameof(Physical.AirDry));
            var ll15 = _context.GetSoilLayerTraitData(layers, nameof(Physical.LL15));
            var dul = _context.GetSoilLayerTraitData(layers, nameof(Physical.DUL));
            var sat = _context.GetSoilLayerTraitData(layers, nameof(Physical.SAT));
            var ks = _context.GetSoilLayerTraitData(layers, nameof(Physical.KS));

            bool valid = request.Report.ValidateItem(thickness, nameof(Physical.Thickness))
                & request.Report.ValidateItem(bd, nameof(Physical.BD))
                & request.Report.ValidateItem(airdry, nameof(Physical.AirDry))
                & request.Report.ValidateItem(ll15, nameof(Physical.LL15))
                & request.Report.ValidateItem(dul, nameof(Physical.DUL))
                & request.Report.ValidateItem(sat, nameof(Physical.SAT))
                & request.Report.ValidateItem(ks, nameof(Physical.KS));

            request.Report.CommitValidation(nameof(Physical), !valid);

            var physical = new Physical()
            {
                Name = nameof(Physical),
                Thickness = thickness,
                BD = bd,
                AirDry = airdry,
                LL15 = ll15,
                DUL = dul,
                SAT = sat,
                KS = ks
            };

            return physical;
        }
    }
}
