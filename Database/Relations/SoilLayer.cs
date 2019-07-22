using System;
using System.Collections.Generic;

namespace Database
{
    public partial class SoilLayer
    {
        public SoilLayer()
        {
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
        }

        public int SoilLayerId { get; set; }
        public int? SoilId { get; set; }
        public int? SoilLayerDepthFrom { get; set; }
        public int? SoilLayerDepthTo { get; set; }

        public virtual Soil Soil { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }
    }
}
