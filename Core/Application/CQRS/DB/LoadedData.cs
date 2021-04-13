using System;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Checks if the database contains any PlotData
    /// </summary>
    public class LoadedData : ContextQuery<bool>
    {
        public class Handler : BaseHandler<LoadedData>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        protected override bool Run()
        {
            return _context.PlotData.Any();
        }
    }
}
