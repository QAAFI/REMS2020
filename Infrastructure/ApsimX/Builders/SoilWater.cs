using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<SoilWater> BuildSoilWater(int plotId)
        {
            return new SoilWater()
            {
                Name = "SoilWater",
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = 1 }),
                SWCON = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["SWCON"] }),
                KLAT = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["KLAT"] })
            };

            // TODO: Single parameters?
        }
    }
}
