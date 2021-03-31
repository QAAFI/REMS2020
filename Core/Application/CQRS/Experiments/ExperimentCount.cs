using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Count the number of experiments in the database
    /// </summary>
    public class ExperimentCount : ContextQuery<int>
    {
        /// <inheritdoc/>
        public class Handler : BaseHandler<ExperimentCount>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override int Run() => _context.Experiments.Count();        
    }
}
