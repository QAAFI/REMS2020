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
    /// Find all the plots in a treatment, represented by the pairing of their ID and Name
    /// </summary>
    public class PlotsQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }
    }

    public class PlotsQueryHandler : IRequestHandler<PlotsQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        private readonly IRemsDbContext _context;

        public PlotsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<KeyValuePair<int, string>>> Handle(PlotsQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private IEnumerable<KeyValuePair<int, string>> Handler(PlotsQuery request, CancellationToken token)
        {
            return _context.Plots
                .Where(p => p.TreatmentId == request.TreatmentId)
                .OrderBy(p => p.Repetition)
                .Select(p => new KeyValuePair<int, string>(p.PlotId, p.Repetition.ToString()))
                .AsEnumerable();
        }
    }
}
