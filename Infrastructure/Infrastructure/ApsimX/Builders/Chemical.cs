using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Chemical> BuildChemicalAnalysis(int soilId)
        {
            return new Chemical()
            {
                Name = "Organic",
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = soilId }),
                Depth = await _mediator.Send(new SoilLayerDepthQuery() { SoilId = soilId }),
                NO3N = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "NO3N" }),
                NH4N = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "NH4N" }),
                PH = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "PH" })
            };
        }
    }
}
