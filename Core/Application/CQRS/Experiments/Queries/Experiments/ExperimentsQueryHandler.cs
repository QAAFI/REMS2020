using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Domain.Entities;
using Rems.Application.Common.Interfaces;
using System.Linq;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class ExperimentsQueryHandler : IRequestHandler<ExperimentsQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        private readonly IRemsDbFactory factory;

        public ExperimentsQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> Handle(ExperimentsQuery request, CancellationToken token)
        {
            return factory.Context.Experiments.Select(e => new KeyValuePair<int, string>(e.ExperimentId, e.Name));
        }
    }
}
