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
            //private readonly IMediator _mediator;

            public Handler(IRemsDbFactory factory)//, IMediator mediator)
            {
                _factory = factory;
                //_mediator = mediator;
            }

            public async Task<Unit> Handle(CreateDBCommand request, CancellationToken cancellationToken)
            {
                _factory.Create(request.FileName);

                //await _mediator.Publish(new DBCreated { }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
