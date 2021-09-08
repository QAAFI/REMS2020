using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Return a collection of all experiments paired by ID and Name
    /// </summary>
    public class ExperimentsQuery : ContextQuery<(int ID, string Name, string Crop)[]>
    {
        /// <inheritdoc/>
        public class Handler : BaseHandler<ExperimentsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override (int, string, string)[] Run() =>
            _context.Experiments
                .Select(e => new Tuple<int, string, string>(e.ExperimentId, e.Name, e.Crop.Name).ToValueTuple())
                .ToArray();
    }
}