using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class SowingSummary : IRequest<Dictionary<string, string>>
    {
        public int ExperimentId { get; set; }
    }

    public class SowingSummaryHandler : IRequestHandler<SowingSummary, Dictionary<string, string>>
    {
        private readonly IRemsDbContext _context;

        public SowingSummaryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Dictionary<string, string>> Handle(SowingSummary request, CancellationToken token) => Task.Run(() => Handler(request));

        private Dictionary<string, string> Handler(SowingSummary request)
        {
            var sow = _context.Experiments.Find(request.ExperimentId).Sowing;

            var d = new Dictionary<string, string>
            {
                { "Method", sow.Method.Name },
                { "Cultivar", sow.Cultivar },
                { "Date", sow.Date.ToString("dd - MM - yyyy") },
                { "Depth", sow.Depth.ToString() },
                { "Row", sow.RowSpace.ToString() },
                { "Pop", sow.Population.ToString() }
            };

            return d;
        }
    }
}
