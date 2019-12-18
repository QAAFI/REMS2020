using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<SoilWater> BuildSoilWater(int soilId)
        {
            return new SoilWater()
            {
                Name = "SoilWater",
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = soilId }),
                SWCON = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = _map["SWCON"] }),
                KLAT = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = _map["KLAT"] }),
                SummerDate = "1-Nov",
                SummerU = 6.0,
                SummerCona = 3.5,
                WinterDate = "1-Apr",
                WinterU = 6.0,
                WinterCona = 3.5,
                Salb = 0.11
            };

            /// TODO: Dynamically assign single parameters
        }
    }
}
