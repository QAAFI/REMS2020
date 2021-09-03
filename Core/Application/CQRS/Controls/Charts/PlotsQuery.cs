using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the plots in a treatment, represented by the pairing of their ID and Name
    /// </summary>
    public class PlotsQuery : ContextQuery<KeyValuePair<int, string>[]>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<PlotsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override KeyValuePair<int, string>[] Run()
        {
            return _context.Plots
                .Where(p => p.TreatmentId == TreatmentId)
                .OrderBy(p => p.Repetition)
                .Select(p => new KeyValuePair<int, string>(p.PlotId, p.Repetition.ToString()))
                .ToArray();
        }
    }
}
