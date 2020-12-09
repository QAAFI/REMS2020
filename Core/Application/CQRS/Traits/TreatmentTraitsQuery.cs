using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    public class TreatmentTraitsQuery : IRequest<string[]> 
    {
        public int TreatmentId { get; set; }
    }

    public class TraitTypesQueryHandler : IRequestHandler<TreatmentTraitsQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public TraitTypesQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(TreatmentTraitsQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private string[] Handler(TreatmentTraitsQuery request, CancellationToken token)
        {
            try
            {
                IEnumerable<string> getTraits(Plot p)
                {
                    var a = p.PlotData.Select(d => d.Trait.Name);
                    var b = p.SoilData.Select(d => d.Trait.Name);
                    var c = p.SoilLayerData.Select(d => d.Trait.Name);

                    return a.Union(b).Union(c);
                };

                var traits = _context.Treatments.Find(request.TreatmentId)
                    .Plots.SelectMany(p => getTraits(p))
                    .Distinct()
                    .ToArray();

                return traits;
            }
            catch
            {
                return new string[] { "FAILURE", "CODE BETTER" };
            }
        }
    }
}
