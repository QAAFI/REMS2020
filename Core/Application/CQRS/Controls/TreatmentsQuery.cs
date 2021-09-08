using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all treatments in an experiment, paired by ID and Name
    /// </summary>
    public class TreatmentsQuery : ContextQuery<(int ID, string Name)[]>
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
        protected override (int, string)[] Run()
        {
            return _context.Experiments.Find(ExperimentId).Treatments
                .Select(t => (t.TreatmentId, t.Name))
                .ToArray();
        }
    }
}
