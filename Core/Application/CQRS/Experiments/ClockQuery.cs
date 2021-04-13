using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM clock model for an experiment
    /// </summary>
    public class ClockQuery : ContextQuery<Clock>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ClockQuery> 
        { 
            public Handler(IRemsDbContextFactory factory) : base(factory) { } 
        }

        /// <inheritdoc/>
        protected override Clock Run()
        {
            var exp = _context.Experiments.Find(ExperimentId);

            bool valid = 
                Report.ValidateItem(exp.BeginDate, nameof(Clock.StartDate))
                & Report.ValidateItem(exp.EndDate, nameof(Clock.EndDate));

            Report.CommitValidation(nameof(Clock), !valid);

            var clock = new Clock()
            {
                Name = "Clock",
                StartDate = exp.BeginDate,
                EndDate = exp.EndDate
            };

            return clock;
        }
    }
}
