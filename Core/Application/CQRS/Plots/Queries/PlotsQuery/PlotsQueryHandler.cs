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
    public class PlotsQueryHandler : IRequestHandler<PlotsQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        private readonly IRemsDbFactory factory;

        public PlotsQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> Handle(PlotsQuery request, CancellationToken token)
        {
            return factory.Context.Plots
                .Where(p => p.TreatmentId == request.TreatmentId)
                .OrderBy(p => p.Repetition)
                .Select(p => new KeyValuePair<int, string>(p.PlotId, p.Repetition.ToString()));
        }
    }
}
