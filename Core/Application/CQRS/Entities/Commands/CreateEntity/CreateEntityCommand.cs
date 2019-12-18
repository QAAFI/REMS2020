using MediatR;

using System.Collections.Generic;

namespace Rems.Application.Entities.Commands
{
    public class CreateEntityCommand : IRequest
    {
        public Dictionary<string, object> Pairs { get; set; }

        // TODO: Make this an enum?
        public string EntityType { get; set; }
    }
}
