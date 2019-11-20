using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Physical> BuildPhysical(int plotId)
        {           
            var model = new Physical()
            {
                Name = "Physical",
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = 1 }),
                BD = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["BD"]}),
                AirDry = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["AirDry"] }),
                LL15 = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["LL15"] }),
                DUL = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["DUL"] }),
                SAT = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["SAT"] }),
                KS = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["KS"] })
            };

            model.Children.Add(await BuildSoilCrop(plotId));

            return model;
        }
    }
}
