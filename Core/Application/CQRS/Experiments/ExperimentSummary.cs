using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Produce a summary of data about an experiment
    /// </summary>
    public class ExperimentSummary : ContextQuery<Dictionary<string, string>>
    {
        /// <summary>
        /// The experiment to summarise
        /// </summary>
        public int ExperimentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ExperimentSummary>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> Run()
        {
            var exp = _context.Experiments.Find(ExperimentId);

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
                { "Start", exp.BeginDate.ToString("dd - MM - yyyy") },
                { "End", exp.EndDate.ToString("dd - MM - yyyy") },
                { "List", list },
                { "Notes", exp.Notes }
            };

            return d;
        }
    }
}
