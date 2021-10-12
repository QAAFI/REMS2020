using System;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM clock model for an experiment
    /// </summary>
    public class CropQuery : ContextQuery<string>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<CropQuery> 
        { 
            public Handler(IRemsDbContextFactory factory) : base(factory) { } 
        }

        /// <inheritdoc/>
        protected override string Run() => _context.Experiments.Find(ExperimentId).Crop.Name;
    }
}
