using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM clock model for an experiment
    /// </summary>
    public class ClockQuery : IRequest<Clock>, IParameterised
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

    public class ClockQueryHandler : IRequestHandler<ClockQuery, Clock>
    {
        private readonly IRemsDbContext _context;

        public ClockQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Clock> Handle(ClockQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private Clock Handler(ClockQuery request)
        {
            var exp = _context.Experiments.Find(request.ExperimentId);

            var clock = new Clock()
            {
                Name = "Clock",
                StartDate = exp.BeginDate,
                EndDate = exp.EndDate
            };

            return clock;
        }
    }
}
