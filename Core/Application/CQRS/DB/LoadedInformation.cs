using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Checks if the database has loaded any fields
    /// </summary>
    public class LoadedInformation : ContextQuery<bool>
    {
        public class Handler : BaseHandler<LoadedInformation>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        protected override bool Run()
        {
            return _context.Fields.Any();
        }
    }
}
