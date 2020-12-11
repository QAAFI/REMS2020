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
    public class SoilTraitsQuery : IRequest<string[]> 
    {
        public int TreatmentId { get; set; }

        public string Type { get; set; }
    }

    public class SoilTraitsQueryHandler : IRequestHandler<SoilTraitsQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public SoilTraitsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(SoilTraitsQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private string[] Handler(SoilTraitsQuery request, CancellationToken token)
        {
            IEnumerable<string> getTraits(Plot p)
            {
                var x = p.SoilData.Select(d => d.Trait.Name);
                var y = p.SoilLayerData.Select(d => d.Trait.Name);

                return x.Union(y);
            };

            var traits = _context.Treatments.Find(request.TreatmentId)
                .Plots.SelectMany(p => getTraits(p))
                .Distinct()
                .ToArray();

            return traits;
        }
    }
}
