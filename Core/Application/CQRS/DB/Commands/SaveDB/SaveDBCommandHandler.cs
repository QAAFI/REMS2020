using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class SaveDBCommandHandler : IRequestHandler<SaveDBCommand>
    {
        IRemsDbFactory _factory;

        public SaveDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<Unit> Handle(SaveDBCommand request, CancellationToken cancellationToken)
        {
            _factory.Context.Save();
            return Unit.Value;
        }
    }
}
