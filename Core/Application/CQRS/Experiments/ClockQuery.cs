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
    public class ClockQuery : IRequest<Clock>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class ClockQueryHandler : IRequestHandler<ClockQuery, Clock>
    {
        private readonly IRemsDbContext _context;

        public ClockQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Clock> Handle(ClockQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private Clock Handler(ClockQuery request)
        {
            var exp = _context.Experiments.Find(request.ExperimentId);

            bool valid = 
                request.Report.ValidateItem(exp.BeginDate, nameof(Clock.StartDate))
                & request.Report.ValidateItem(exp.EndDate, nameof(Clock.EndDate));

            request.Report.CommitValidation(nameof(Clock), !valid);

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
