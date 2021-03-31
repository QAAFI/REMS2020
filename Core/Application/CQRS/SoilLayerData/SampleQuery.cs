using System;
using System.Linq;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Sample model for an experiment
    /// </summary>
    public class SampleQuery : ContextQuery<Sample>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SampleQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Sample Run()
        {
            var layers = _context.GetSoilLayers(ExperimentId);

            var sample = new Sample()
            {
                Name = "Initial water",
                Depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth ?? 0}").ToArray(),
                Thickness = layers.Select(l => (double)((l.ToDepth ?? 0) - (l.FromDepth ?? 0))).ToArray(),
                SW = _context.GetSoilLayerTraitData(layers, "SW")
            };

            return sample;
        }
    }
}
