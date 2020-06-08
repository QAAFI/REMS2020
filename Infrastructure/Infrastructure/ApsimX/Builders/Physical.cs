using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Physical> BuildPhysical(int soilId)
        {           
            var model = new Physical()
            {
                Name = "Physical",
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = soilId }),
                BD = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "BD"}),
                AirDry = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "AirDry" }),
                LL15 = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "LL15" }),
                DUL = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "DUL" }),
                SAT = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "SAT" }),
                KS = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "KS" })
            };

            model.Children.Add(await BuildSoilCrop(soilId));

            return model;
        }
    }
}
