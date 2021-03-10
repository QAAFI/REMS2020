using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Core;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Zone model for an experiment
    /// </summary>
    public class ZoneQuery : IRequest<Zone>, IParameterised
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
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

    public class ZoneQueryHandler : IRequestHandler<ZoneQuery, Zone>
    {
        private readonly IRemsDbContext _context;

        public ZoneQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Zone> Handle(ZoneQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private Zone Handler(ZoneQuery request)
        {
            var field = _context.Experiments.Find(request.ExperimentId).Field;

            var zone = new Zone()
            {
                Name = "Field",
                Slope = field.Slope.GetValueOrDefault()
            };

            return zone;
        }
    }
}
