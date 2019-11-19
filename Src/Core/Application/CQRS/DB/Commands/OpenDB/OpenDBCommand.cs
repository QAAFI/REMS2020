using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class OpenDBCommand : IRequest<IRemsDbContext>
    {
        public string FileName { get; set; }

        public class Handler : IRequestHandler<OpenDBCommand, IRemsDbContext>
        {
            private readonly IRemsDbFactory _factory;

            public Handler(IRemsDbFactory factory)
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
}
