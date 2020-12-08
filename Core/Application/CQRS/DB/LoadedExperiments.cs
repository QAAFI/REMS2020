using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.CQRS
{
    public class LoadedExperiments : IRequest<bool>
    { }

    public class LoadedExperimentsHandler : IRequestHandler<LoadedExperiments, bool>
    {
        private readonly IRemsDbContext _context;

        public LoadedExperimentsHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(LoadedExperiments request, CancellationToken cancellationToken) 
            => Task.Run(() => Handler(request));

        private bool Handler(LoadedExperiments request)
        {
            return _context.Experiments.Any();
        }
    }
}
