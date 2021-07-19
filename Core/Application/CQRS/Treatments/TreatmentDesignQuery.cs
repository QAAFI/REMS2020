using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all treatments in an experiment, paired by ID and Name
    /// </summary>
    public class TreatmentDesignQuery : ContextQuery<string>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<TreatmentDesignQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override string Run()
        {
            if (_context.Treatments.Find(TreatmentId) is not Treatment treatment)
                return "";

            var names = treatment.Designs.Select(d => $"{d.Level.Factor.Name} {d.Level.Name}");

            return string.Join(" x ", names);
        }
    }
}
