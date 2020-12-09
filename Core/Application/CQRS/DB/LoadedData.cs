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
    public class LoadedData : IRequest<bool>
    { }

    public class LoadedDataHandler : IRequestHandler<LoadedData, bool>
    {
        private readonly IRemsDbContext _context;

        public LoadedDataHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(LoadedData request, CancellationToken cancellationToken) 
            => Task.Run(() => Handler(request));

        private bool Handler(LoadedData request)
        {
            return _context.PlotData.Any();
        }
    }
}
