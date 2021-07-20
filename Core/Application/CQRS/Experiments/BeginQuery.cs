using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Count the number of experiments in the database
    /// </summary>
    public class BeginQuery : ContextQuery<DateTime>
    {
        public int ID { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<BeginQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DateTime Run() 
            => _context.Experiments.Find(ID)?.BeginDate 
            ?? new DateTime(DateTime.Now.Year - 1, 1, 1);        
    }
}
