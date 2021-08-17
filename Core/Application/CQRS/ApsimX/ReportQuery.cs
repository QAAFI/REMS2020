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
    /// Generates an APSIM report model for an experiment
    /// </summary>
    public class ReportQuery : ContextQuery<Report>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ReportQuery> 
        { 
            public Handler(IRemsDbContextFactory factory) : base(factory) { } 
        }

        /// <inheritdoc/>
        protected override Report Run()
        {
            string crop = _context.Experiments.Find(ExperimentId).Crop.Name;
            
            var report = new Report
            {
                Name = "HarvestReport",
                VariableNames = new string[]
                {
                    "[Clock].Today",
                    $"[{crop}].Phenology.Stage",
                    $"[{crop}].Phenology.CurrentStageName",
                    $"[{crop}].AboveGround.Wt",
                    $"[{crop}].AboveGround.N"
                },
                EventNames = new string[] { $"[{crop}].Harvesting" }
            };

            return report;
        }
    }
}
