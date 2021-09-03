using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all treatments in an experiment, paired by ID and Name
    /// </summary>
    public class TreatmentsQuery : ContextQuery<KeyValuePair<int, string>[]>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<TreatmentsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override KeyValuePair<int, string>[] Run()
        {
            return _context.Experiments.Find(ExperimentId).Treatments
                .Select(t => new KeyValuePair<int, string>(t.TreatmentId, t.Name))
                .ToArray();
        }
    }
}
