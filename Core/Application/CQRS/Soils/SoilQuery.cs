using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Soils;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class SoilQuery : IRequest<Soil>, IParameterised
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
