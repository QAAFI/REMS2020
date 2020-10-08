using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.CQRS
{
    public class ExperimentCount : IRequest<int>
    {
    }

    public class ExperimentCountHandler : IRequestHandler<ExperimentCount, int>
    {
        private readonly IRemsDbContext _context;

        public ExperimentCountHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<int> Handle(ExperimentCount request, CancellationToken cancellationToken)
            => Task.Run(() => _context.Experiments.Count());
    }
}
