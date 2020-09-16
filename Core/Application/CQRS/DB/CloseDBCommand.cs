using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class CloseDBCommand : IRequest
    { }

    public class CloseDBCommandHandler : IRequestHandler<CloseDBCommand>
    {
        private readonly IRemsDbContext _context;

        public CloseDBCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(CloseDBCommand request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));

        private Unit Handler(CloseDBCommand request)
        {
            _context.Close(); 
            return Unit.Value;
        }
    }
}
