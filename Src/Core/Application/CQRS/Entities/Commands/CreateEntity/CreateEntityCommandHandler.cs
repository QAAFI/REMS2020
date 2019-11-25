using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using System;
using System.Threading;
using System.Threading.Tasks;


namespace Rems.Application.Entities.Commands
{
    public class CreateEntityCommandHandler : IRequestHandler<CreateEntityCommand>
    {
        private readonly IRemsDbFactory _factory;

        public CreateEntityCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<MediatR.Unit> Handle(CreateEntityCommand request, CancellationToken token)
        {
            try
            {
                //CreateInstance is likely to cause an exception on any typo - use northwind example for error handling 
                IEntity entity = Activator.CreateInstance(Type.GetType(request.EntityType)) as IEntity;

                //var entity = EntityFactory.Create(request.EntityType);
                entity.Update(request.Pairs);

                _factory.Context.Add(entity);
                _factory.Context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MediatR.Unit.Value;
        }
    }
}
