using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Checks if the database contains an experiments
    /// </summary>
    public class LoadedExperiments : ContextQuery<bool>
    {
        public class Handler : BaseHandler<LoadedExperiments>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        protected override bool Run()
        {
            return _context.Experiments.Any();
        }
    }
}
