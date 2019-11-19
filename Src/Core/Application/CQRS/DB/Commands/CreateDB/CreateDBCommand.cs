using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class CreateDBCommand : IRequest<IRemsDbContext>
    {
        public string FileName { get; set; }

        public class Handler : IRequestHandler<CreateDBCommand, IRemsDbContext>
        {
            private readonly IRemsDbFactory _factory;

            public Handler(IRemsDbFactory factory)
            {
                _factory = factory;
            }

            public async Task<IRemsDbContext> Handle(CreateDBCommand request, CancellationToken cancellationToken)
            {
                _factory.Create(request.FileName);
                return _factory.Context;
            }
        }
    }
}
