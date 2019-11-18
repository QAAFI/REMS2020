using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Models;
using Rems.Domain;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Rems.Application.Entities.Commands.InsertEntity
{
    public class CreateEntityCommand : IRequest
    {
        public Dictionary<string, object> Pairs { get; set; }

        // TODO: Make this an enum?
        public string EntityType { get; set; }

        public class Handler : IRequestHandler<CreateEntityCommand>
        {
            private readonly IRemsDbContext _context;

            public Handler(IRemsDbContext context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<MediatR.Unit> Handle(CreateEntityCommand request, CancellationToken token)
            {
                var entity = EntityFactory.Create(request.EntityType);
                entity.Update(request.Pairs);

                _context.Add(entity);
                await _context.SaveChangesAsync(token);

                return MediatR.Unit.Value;
            }
        }
    }
}
