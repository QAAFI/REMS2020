using MediatR;

using Rems.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class OpenDBCommandHandler : IRequestHandler<OpenDBCommand, IRemsDbContext>
    {
        private readonly IRemsDbFactory _factory;

        public OpenDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<IRemsDbContext> Handle(OpenDBCommand request, CancellationToken cancellationToken)
        {
            _factory.Open(request.FileName);
            return _factory.Context;
        }
    }
}
