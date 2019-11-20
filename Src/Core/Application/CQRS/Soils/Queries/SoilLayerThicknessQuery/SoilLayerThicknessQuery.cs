using MediatR;

using System.Collections.Generic;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerThicknessQuery : IRequest<IEnumerable<double>>
    {
        public int SoilId { get; set; }
    }
}