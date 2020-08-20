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
    public class TreatmentsQueryHandler : IRequestHandler<TreatmentsQuery, KeyValuePair<int, string>[]>
    {
        private readonly IRemsDbContext _context;

        public TreatmentsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<KeyValuePair<int, string>[]> Handle(TreatmentsQuery request, CancellationToken token)
        {
            return Task.Run(() => _context.Treatments
                .Where(t => t.ExperimentId == request.ExperimentId)
                .Select(t => new KeyValuePair<int, string>(t.TreatmentId, t.Name))
                .ToArray()
            );
        }
    }
}
