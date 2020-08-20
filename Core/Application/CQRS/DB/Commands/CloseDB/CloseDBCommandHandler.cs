using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class CloseDBCommandHandler : IRequestHandler<CloseDBCommand>
    {
        IRemsDbContext _context;

        public CloseDBCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(CloseDBCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _context.Close();
                return Unit.Value;
            });            
        }
    }
}
