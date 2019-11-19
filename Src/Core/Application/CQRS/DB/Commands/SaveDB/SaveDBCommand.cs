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
    public class SaveDBCommand : IRequest
    {
        public IRemsDbContext Context { get; set; }

        public class Handler : IRequestHandler<SaveDBCommand>
        {
            public Handler()
            {
            }

            public async Task<Unit> Handle(SaveDBCommand request, CancellationToken cancellationToken)
            {
                request.Context.Save();
                return Unit.Value;
            }
        }
    }
}
