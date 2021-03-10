using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Checks if the database has loaded any fields
    /// </summary>
    public class LoadedInformation : IRequest<bool>
    { }

    public class LoadedInformationHandler : IRequestHandler<LoadedInformation, bool>
    {
        private readonly IRemsDbContext _context;

        public LoadedInformationHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(LoadedInformation request, CancellationToken cancellationToken) 
            => Task.Run(() => Handler(request));

        private bool Handler(LoadedInformation request)
        {
            return _context.Fields.Any();
        }
    }
}
