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
    public class TreatmentsQueryHandler : IRequestHandler<TreatmentsQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        private readonly IRemsDbFactory factory;

        public TreatmentsQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> Handle(TreatmentsQuery request, CancellationToken token)
        {
            return factory.Context.Treatments
                .Where(t => t.ExperimentId == request.ExperimentId)
                .Select(t => new KeyValuePair<int, string>(t.TreatmentId, t.Name));
        }
    }
}
