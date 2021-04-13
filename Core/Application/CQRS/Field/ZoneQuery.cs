using System;
using Models.Core;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Zone model for an experiment
    /// </summary>
    public class ZoneQuery : ContextQuery<Zone>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ZoneQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Zone Run()
        {
            var field = _context.Experiments.Find(ExperimentId).Field;
            var slope = field.Slope.GetValueOrDefault();

            var valid = Report.ValidateItem(field.Name, nameof(Zone.Name))
                & Report.ValidateItem(slope, nameof(Zone.Slope));

            Report.CommitValidation(nameof(Zone), !valid);

            var zone = new Zone
            {
                Name = field.Name,
                Slope = slope,
                Area = 1
            };

            return zone;
        }
    }
}
