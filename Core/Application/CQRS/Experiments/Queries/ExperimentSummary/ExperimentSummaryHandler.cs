using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Domain.Entities;
using Rems.Application.Common.Interfaces;
using System.Linq;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class ExperimentSummaryHandler : IRequestHandler<ExperimentSummary, Dictionary<string, string>>
    {
        private readonly IRemsDbContext _context;

        public ExperimentSummaryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Dictionary<string, string>> Handle(ExperimentSummary request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Dictionary<string, string> Handler(ExperimentSummary request)
        {
            var exp = _context.Experiments.Find(request.ExperimentId);

            var researchers = exp.ResearcherList.Select(r => r.Researcher.Name + "\n");
            string list = string.Concat(researchers);

            var d = new Dictionary<string, string>
            {
                { "Description", exp.Description },
                { "Design", exp.Design },
                { "Crop", exp.Crop.Name },
                { "Field", exp.Field.Name },
                { "Met", exp.MetStation.Name },
                { "Reps", exp.Repetitions.ToString() },
                { "Rating", exp.Rating.ToString() },
                { "Start", exp.BeginDate.Value.ToString("dd - MM - yyyy") },
                { "End", exp.EndDate.Value.ToString("dd - MM - yyyy") },
                { "List", list },
                { "Notes", exp.Notes }
            };

            return d;
        }
    }
}
