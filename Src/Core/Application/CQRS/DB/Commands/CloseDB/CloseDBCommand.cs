using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class CloseDBCommand : IRequest
    {
        public IRemsDbContext Context { get; set; }

        public class Handler : IRequestHandler<CloseDBCommand>
        {
            public Handler()
            {
            }

            public async Task<Unit> Handle(CloseDBCommand request, CancellationToken cancellationToken)
            {
                request.Context.Close();
                return Unit.Value;
            }
        }
    }
}
