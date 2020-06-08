using Models.Soils;

using Rems.Application.Soils.Queries;

using System.Threading.Tasks;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Sample> BuildSample(string name, int soilId)
        {
            return new Sample()
            {
                Name = name,
                Depth = await _mediator.Send(new SoilLayerDepthQuery() { SoilId = soilId}),
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = soilId }),
                SW = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "SW" })
            };
        }
    }
}
