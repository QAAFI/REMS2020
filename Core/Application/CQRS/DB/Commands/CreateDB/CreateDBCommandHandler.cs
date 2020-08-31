using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class CreateDBCommandHandler : IRequestHandler<CreateDBCommand, IRemsDbContext>
    {
        private readonly IRemsDbFactory _factory;

        public CreateDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<IRemsDbContext> Handle(CreateDBCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _factory.Create(request.FileName));
        }
    }
}
