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
            var sample = new Sample()
            {
                Name = "Initial conditions",
                Depth = new[] { "0-180" },
                Thickness = new[] { 1800.0 },
                NH4 = new[] { 1.0 }
            };

            return sample;
        }
    }
}
