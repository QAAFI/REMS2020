using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Information, "SoilLayers")]
    public class SoilLayer : IEntity
    {
        public SoilLayer()
        {
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
        }

        public int SoilLayerId { get; set; }

        public int SoilId { get; set; }

        [Expected("FromDepth", "DepthFrom")]
        public int? FromDepth { get; set; }

        [Expected("ToDepth", "DepthTo")]
        public int ToDepth { get; set; }

        [Expected("Soil", "SoilType")]
        public virtual Soil Soil { get; set; }

        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }


    }
}
