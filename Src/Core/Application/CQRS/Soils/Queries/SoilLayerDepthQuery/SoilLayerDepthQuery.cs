using MediatR;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerDepthQuery : IRequest<string[]>
    {
        public int SoilId { get; set; }
    }
}