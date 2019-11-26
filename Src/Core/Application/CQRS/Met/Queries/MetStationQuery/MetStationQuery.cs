using MediatR;

namespace Rems.Application.Met.Queries
{
    public class MetStationQuery : IRequest<MetStationDto>
    {
        public int Id { get; set; }
    }
}
