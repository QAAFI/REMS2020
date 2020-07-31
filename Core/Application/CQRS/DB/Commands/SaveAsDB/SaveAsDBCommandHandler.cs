using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class SaveAsDBCommandHandler : IRequestHandler<SaveAsDBCommand>
    {
        IRemsDbFactory _factory;

        public SaveAsDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<Unit> Handle(SaveAsDBCommand request, CancellationToken cancellationToken)
        {
            _factory.Context.SaveAs(request.FileName);
            return Unit.Value;
        }
    }
}
