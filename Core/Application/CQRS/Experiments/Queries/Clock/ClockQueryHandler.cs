using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;
using Models.Soils;
using Rems.Application.Common.Extensions;
using Models.WaterModel;
using Models.Core;
using Models;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class ClockQueryHandler : IRequestHandler<ClockQuery, Clock>
    {
        private readonly IRemsDbContext _context;

        public ClockQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Clock> Handle(ClockQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Clock Handler(ClockQuery request)
        {
            var exp = _context.Experiments.Find(request.ExperimentId);

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