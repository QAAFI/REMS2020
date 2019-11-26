using MediatR;
using Models.Core;

namespace Rems.Infrastructure.ApsimX
{
    public class ApsimX : IApsimX
    {
        public ApsimBuilder Builder { get; set; }

        public Simulations Simulations { get; set; } = new Simulations();

        public ApsimX(IMediator mediator)
        {
            Builder = new ApsimBuilder(mediator);
        }
    }
}
