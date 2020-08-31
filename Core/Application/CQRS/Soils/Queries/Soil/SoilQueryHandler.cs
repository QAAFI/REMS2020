using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using Models.Soils;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class SoilQueryHandler : IRequestHandler<SoilQuery, Soil>
    {
        private readonly IRemsDbContext _context;

        public SoilQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Soil> Handle(SoilQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Soil Handler(SoilQuery request)
        {
            var field = _context.Experiments.Find(request.ExperimentId).Field;

            var soil = new Soil()
            {
                Name = "Soil",
                Latitude = field.Latitude.GetValueOrDefault(),
                Longitude = field.Longitude.GetValueOrDefault()                
            };

            return soil;
        }
    }
}