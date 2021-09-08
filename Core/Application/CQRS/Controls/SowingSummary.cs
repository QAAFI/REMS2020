using System;
using System.Collections.Generic;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Produce a summary of sowing information for an experiment
    /// </summary>
    public class SowingSummary : ContextQuery<Dictionary<string, string>>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SowingSummary>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> Run()
        {
            var sow = _context.Experiments.Find(ExperimentId).Sowing;

            var d = new Dictionary<string, string>
            {
                { "Method", sow?.Method.Name },
                { "Cultivar", sow?.Cultivar },
                { "Date", sow?.Date.ToString("dd - MM - yyyy") },
                { "Depth", sow?.Depth.ToString() },
                { "Row", sow?.RowSpace.ToString() },
                { "Pop", sow?.Population.ToString() }
            };

            return d;
        }
    }
}
