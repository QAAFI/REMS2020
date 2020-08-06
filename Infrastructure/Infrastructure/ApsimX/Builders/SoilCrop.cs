﻿using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Soils.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<SoilCrop> BuildSoilCrop(int soilId)
        {
            return new SoilCrop()
            {
                Name = "SorghumSoil",
                LL = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "LL" }),
                KL = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "KL" }),
                XF = await _mediator.Send(new SoilLayerTraitQuery() { SoilId = soilId, TraitName = "XF" })
            };
        }
    }
}