using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<SoilCrop> BuildSoilCrop(int plotId)
        {
            return new SoilCrop()
            {
                Name = "SoilCrop",
                LL = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["LL"] }),
                KL = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["KL"] }),
                XF = await _mediator.Send(new SoilLayerDataQuery() { PlotId = plotId, TraitName = _map["XF"] })
            };
        }
    }
}
