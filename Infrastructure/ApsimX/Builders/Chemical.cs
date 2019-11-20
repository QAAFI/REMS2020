using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Chemical> BuildChemicalAnalysis(int plotId)
        {
            return new Chemical()
            {
                Name = "Organic",
                Thickness = await _mediator.Send(new SoilLayerThicknessQuery() { SoilId = 1 }),
                Depth = await _mediator.Send(new SoilLayerDepthQuery() { SoilId = 1 }),
                NO3N = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["NO3N"] }),
                NH4N = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["NH4N"] }),
                PH = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["PH"] })
            };
        }
    }
}
