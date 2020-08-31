using MediatR;

namespace Rems.Application.Met.Queries
{
    public class MetStationQuery : IRequest<string>
    {
        public int ExperimentId { get; set; }
    }
}
