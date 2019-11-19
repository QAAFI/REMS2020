using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Models;
using Rems.Domain;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Rems.Application.Entities.Commands.BulkInsert
{
    public class BulkInsertCommand : IRequest
    {
        public List<Dictionary<string, object>> Data { get; set; }

        public string EntityType { get; set; }

        public class Handler : IRequestHandler<BulkInsertCommand>
        {
            private readonly IRemsDbContext _context;

            public Handler(IRemsDbContext context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<MediatR.Unit> Handle(BulkInsertCommand request, CancellationToken token)
            {

                var entities = request.Data.Select(d => 
                {
                    //CreateInstance is likely to cause an exception on any typo - use northwind example for error handling 
                    IEntity entity = Activator.CreateInstance(Type.GetType(request.EntityType)) as IEntity;
                    entity.Update(d);
                    return entity;                
                });                

                _context.AddRange(entities);
                await _context.SaveChangesAsync(token);

                return MediatR.Unit.Value;
            }
        }
    }
}
