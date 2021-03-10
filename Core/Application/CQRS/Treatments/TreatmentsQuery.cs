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
    /// Find all treatments in an experiment, paired by ID and Name
    /// </summary>
    public class TreatmentsQuery : IRequest<KeyValuePair<int, string>[]>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }
    }

    public class TreatmentsQueryHandler : IRequestHandler<TreatmentsQuery, KeyValuePair<int, string>[]>
    {
        private readonly IRemsDbContext _context;

        public TreatmentsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<KeyValuePair<int, string>[]> Handle(TreatmentsQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private KeyValuePair<int, string>[] Handler(TreatmentsQuery request, CancellationToken token)
        {
            return _context.Treatments
                .Where(t => t.ExperimentId == request.ExperimentId)
                .Select(t => new KeyValuePair<int, string>(t.TreatmentId, t.Name))
                .ToArray();
        }
    }
}
