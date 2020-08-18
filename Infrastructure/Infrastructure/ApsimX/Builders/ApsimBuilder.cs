using MediatR;

using Rems.Application.Common.Mappings;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        private readonly IMediator _mediator;

        public ApsimBuilder(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
