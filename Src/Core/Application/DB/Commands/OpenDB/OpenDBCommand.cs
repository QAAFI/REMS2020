using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands.OpenDB
{
    public class OpenDBCommand : IRequest
    {
        public string FileName { get; set; }

        public class Handler : IRequestHandler<OpenDBCommand>
        {
            private readonly IRemsDbFactory _factory;

            public Handler(IRemsDbFactory factory)
            {
                _factory = factory;
            }

            public async Task<Unit> Handle(OpenDBCommand request, CancellationToken cancellationToken)
            {
                _factory.Create(request.FileName);
                return Unit.Value;
            }
        }
    }
}
