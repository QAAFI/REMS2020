using MediatR;

using Rems.Application.Common.Mappings;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        private readonly IMediator _mediator;
        private readonly IPropertyMap _map = Settings.Instance["TRAITS"];
    }
}
