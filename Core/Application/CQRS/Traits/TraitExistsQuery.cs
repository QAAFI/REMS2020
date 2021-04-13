using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Check if a trait exists in the database
    /// </summary>
    public class TraitExistsQuery : ContextQuery<bool>
    {
        /// <summary>
        /// The trait name
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<TraitExistsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }        

        protected override bool Run()
        {
            var result = _context?.Traits.Any(t => t.Name == Name) ?? false;
            return result;
        }
    }
}