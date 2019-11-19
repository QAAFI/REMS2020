using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands.CreateDB
{
    public class CreateDBCommand : IRequest
    {
        public string FileName { get; set; }

        public class Handler : IRequestHandler<CreateDBCommand>
        {
            private readonly IRemsDbFactory _factory;

            public Handler(IRemsDbFactory factory)
            {
                _factory = factory;
            }

            public async Task<Unit> Handle(CreateDBCommand request, CancellationToken cancellationToken)
            {
                _factory.Create(request.FileName);
                return Unit.Value;
            }
        }
    }
}
