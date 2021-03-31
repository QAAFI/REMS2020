using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find the names of all the tables in the database
    /// </summary>
    public class GetTableNamesQuery : ContextQuery<string[]>
    {

        /// <inheritdoc/>
        public class Handler : BaseHandler<GetTableNamesQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override string[] Run() => _context.Names.ToArray();
    }
}
