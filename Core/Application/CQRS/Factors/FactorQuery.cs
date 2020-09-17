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
    public class FactorQuery : IRequest<Factor[]>
    {
        public int ExperimentId { get; set; }
    }

    public class FactorQueryHandler : IRequestHandler<FactorQuery, Factor[]>
    {
        private readonly IRemsDbContext _context;

        public FactorQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Factor[]> Handle(FactorQuery request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private Factor[] Handler(FactorQuery request, CancellationToken token)
        {
            var models = new List<Factor>();

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
                    model.Children.Add(new Memo() { Name = level.Name });
                }

                models.Add(model);
            }

            return models.ToArray();
        }
    }
}