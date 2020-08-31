using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class SaveDBCommandHandler : IRequestHandler<SaveDBCommand>
    {
        private readonly IRemsDbContext _context;

        public SaveDBCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(SaveDBCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _context.Save();
                return Unit.Value;
            });
        }
    }
}
