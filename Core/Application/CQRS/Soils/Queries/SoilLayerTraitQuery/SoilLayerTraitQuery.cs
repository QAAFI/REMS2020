using MediatR;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerTraitQuery : IRequest<double[]>
    {
        public int SoilId { get; set; }

        public string TraitName { get; set; }
    }
}