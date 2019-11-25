using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class CloseDBCommandHandler : IRequestHandler<CloseDBCommand>
    {
        IRemsDbFactory _factory;

        public CloseDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<Unit> Handle(CloseDBCommand request, CancellationToken cancellationToken)
        {
            _factory.Context.Close();
            return Unit.Value;
        }
    }
}
