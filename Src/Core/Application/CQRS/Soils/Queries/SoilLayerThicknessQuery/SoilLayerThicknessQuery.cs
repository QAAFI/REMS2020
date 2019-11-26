using MediatR;

using System.Collections.Generic;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerThicknessQuery : IRequest<double[]>
    {
        public int SoilId { get; set; }
    }
}