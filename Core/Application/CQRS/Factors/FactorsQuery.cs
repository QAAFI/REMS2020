using MediatR;
using Models;
using Models.Factorial;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using REMSFactor = Rems.Domain.Entities.Factor;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Permutation model for an experiment
    /// </summary>
    public class FactorsQuery : IRequest<Factors>, IParameterised
    {
        /// <summary>
        /// The experiment to model
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

    public class FactorQueryHandler : IRequestHandler<FactorsQuery, Factors>
    {
        private readonly IRemsDbContext _context;

        public FactorQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Factors> Handle(FactorsQuery request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private Factors Handler(FactorsQuery request, CancellationToken token)
        {
            var factors = new Factors { Name = "Factors" };

            var designs = _context.Designs.Where(d => d.Treatment.ExperimentId == request.ExperimentId).ToArray();
            var fs = designs.Select(d => d.Level.Factor).Distinct().ToArray();

            fs.ForEach(f =>
            {
                var factor = new Factor { Name = f.Name };

                designs.Select(d => d.Level)                    
                    .Where(l => l.Factor == f)
                    .Distinct()
                    .ForEach(l => factor.Children.Add(new CompositeFactor { Name = l.Name, Specifications = new List<string>() }));

                factors.Children.Add(factor);
            });

            return factors;
        }
    }
}