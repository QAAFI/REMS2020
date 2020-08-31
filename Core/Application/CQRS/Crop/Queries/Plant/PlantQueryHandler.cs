using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.PMF;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlantQueryHandler : IRequestHandler<PlantQuery, Plant>
    {
        private readonly IRemsDbContext _context;

        public PlantQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Plant> Handle(PlantQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Plant Handler(PlantQuery request)
        {
            var crop = _context.Experiments.Find(request.ExperimentId).Crop;

            var plant = new Plant()
            {
                CropType = crop.Name,
                Name = crop.Name,
                ResourceName = crop.Name                
            };

            return plant;
        }
    }
}