using MediatR;
using Models.Core;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Rems.Infrastructure.ApsimX
{
    public class ApsimX : IApsimX
    {
        public IMediator Mediator { get; set; }

        public Simulations Simulations { get; set; } = new Simulations();

        public ApsimX(IMediator mediator)
        {
            Mediator = mediator;            
        }

    }
}
