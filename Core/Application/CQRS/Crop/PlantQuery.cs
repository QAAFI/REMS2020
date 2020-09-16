using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.PMF;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class PlantQuery : IRequest<Plant>, IParameterised
    {   
        public int ExperimentId { get; set; }

        public void Parameterise(params object[] args)
        {
            if (args.Length != 1) 
                throw new Exception($"Invalid number of parameters. \n Expected: 1 \n Received: {args.Length}");

            if (args[0] is int id)
                ExperimentId = id;
            else
                throw new Exception($"Invalid parameter type. \n Expected: {typeof(int)} \n Received: {args[0].GetType()}");
        }
    }

    public class PlantQueryHandler : IRequestHandler<PlantQuery, Plant>
    {
        private readonly IRemsDbContext _context;

        public PlantQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Plant> Handle(PlantQuery request, CancellationToken token) => Task.Run(() => Handler(request));
        
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
