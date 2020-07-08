using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class SoilLayer : IEntity
    {
        public SoilLayer()
        {
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
        }

        public int SoilLayerId { get; set; }

        public int SoilId { get; set; }

        public int? FromDepth { get; set; } = null;

        public int? ToDepth { get; set; } = null;


        public virtual Soil Soil { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }


    }
}
