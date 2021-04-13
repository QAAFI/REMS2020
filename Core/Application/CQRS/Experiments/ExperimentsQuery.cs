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
    public class ExperimentsQuery : ContextQuery<KeyValuePair<int, string>[]>
    {
        /// <inheritdoc/>
        public class Handler : BaseHandler<ExperimentsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override KeyValuePair<int, string>[] Run() =>
            _context.Experiments
                .Select(e => new KeyValuePair<int, string>(e.ExperimentId, e.Name))
                .ToArray();
    }
}