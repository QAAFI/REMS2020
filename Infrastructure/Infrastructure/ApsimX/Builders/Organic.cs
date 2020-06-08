using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Organic> BuildSoilOrganicMatter(int soilId)
        {
            return new Organic()
            {
                Name = "Organic",
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = soilId }),
                Carbon = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "Carbon" }),
                SoilCNRatio = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "SoilCNRatio" }),
                FBiom = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "FBiom" }),
                FInert = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "FInert" }),
                FOM = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "FOM" })
            };
        }
    }
}
