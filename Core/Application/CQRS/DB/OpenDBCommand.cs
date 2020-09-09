using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class OpenDBCommand : IRequest<Unit>
    {
        public string FileName { get; set; }        
    }

    public class OpenDBCommandHandler : IRequestHandler<OpenDBCommand, Unit>
    {
        private readonly IRemsDbFactory _factory;

        public OpenDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<Unit> Handle(OpenDBCommand request, CancellationToken cancellationToken)
        {
            _factory.Connection = request.FileName;
            return Task.Run(() => Unit.Value);
        }
    }
}
