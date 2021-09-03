using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Permutation model for an experiment
    /// </summary>
    public class FactorExistsQuery : ContextQuery<bool>
    { 
        /// <summary>
        /// The name of the factor to look for
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<FactorExistsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override bool Run() => _context.Factors.Any(f => f.Name == Name);
    }
}