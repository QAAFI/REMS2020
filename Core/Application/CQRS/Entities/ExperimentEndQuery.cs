using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Count the number of experiments in the database
    /// </summary>
    public class ExperimentEndQuery : ContextQuery<DateTime>
    {
        public int ID { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ExperimentEndQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DateTime Run() 
            => _context.Experiments.Find(ID)?.EndDate
            ?? DateTime.Now;
    }
}
