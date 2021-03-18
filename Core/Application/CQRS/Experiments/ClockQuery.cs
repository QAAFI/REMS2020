﻿using System;
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

            var clock = new Clock()
            {
                Name = "Clock",
                StartDate = request.Report.ValidateItem(exp.BeginDate, "Clock.StartDate"),
                EndDate = request.Report.ValidateItem(exp.EndDate, "Clock.EndDate")
            };

            return clock;
        }
    }
}
