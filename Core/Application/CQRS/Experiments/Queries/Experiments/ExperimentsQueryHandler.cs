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
    public class ExperimentsQueryHandler : IRequestHandler<ExperimentsQuery, KeyValuePair<int, string>[]>
    {
        private readonly IRemsDbContext _context;

        public ExperimentsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<KeyValuePair<int, string>[]> Handle(ExperimentsQuery request, CancellationToken token)
        {
            return Task.Run(() => _context.Experiments
                .Select(e => new KeyValuePair<int, string>(e.ExperimentId, e.Name))
                .ToArray()
            );
        }
    }
}
