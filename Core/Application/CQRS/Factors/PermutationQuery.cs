using MediatR;
using Models;
using Models.Factorial;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.CQRS
{
    public class PermutationQuery : IRequest<Permutation>, IParameterised
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

    public class FactorQueryHandler : IRequestHandler<PermutationQuery, Permutation>
    {
        private readonly IRemsDbContext _context;

        public FactorQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Permutation> Handle(PermutationQuery request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private Permutation Handler(PermutationQuery request, CancellationToken token)
        {
            var permutation = new Permutation();

            var levels = _context.Experiments.Find(request.ExperimentId)
                .Treatments
                .SelectMany(t => t.Designs)
                .Select(d => d.Level)
                .Distinct()
                .ToArray(); // Necessary for GroupBy

            var factors = levels.GroupBy(l => l.Factor);

            foreach (var factor in factors)
            {
                var entity = factor.Key;

                var model = new Factor()
                {
                    Name = entity.Name
                };

                foreach (var level in factor)
                {
                    model.Children.Add(new CompositeFactor() { Name = level.Name });
                }

                permutation.Children.Add(model);
            }

            return permutation;
        }
    }
}