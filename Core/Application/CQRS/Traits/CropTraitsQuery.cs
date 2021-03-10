using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the crop traits with data in a treatment
    /// </summary>
    public class CropTraitsQuery : IRequest<string[]> 
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }
    }

    public class CropTraitsQueryHandler : IRequestHandler<CropTraitsQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public CropTraitsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(CropTraitsQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private string[] Handler(CropTraitsQuery request, CancellationToken token)
        {
            var traits = _context.Treatments.Find(request.TreatmentId)
                .Plots.SelectMany(p => p.PlotData.Select(d => d.Trait.Name))
                .Distinct()
                .ToArray();

            return traits;
        }
    }
}
