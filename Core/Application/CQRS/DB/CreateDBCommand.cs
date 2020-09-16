using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class CreateDBCommand : IRequest<IRemsDbContext>
    {
        public string FileName { get; set; }        
    }

    public class CreateDBCommandHandler : IRequestHandler<CreateDBCommand, IRemsDbContext>
    {
        private readonly IRemsDbFactory _factory;

        public CreateDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<IRemsDbContext> Handle(CreateDBCommand request, CancellationToken cancellationToken) => Task.Run(() => _factory.Create(request.FileName));
    }
}
