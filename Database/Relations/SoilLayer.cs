using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("SoilLayer")]
    public class SoilLayer
    {
        public SoilLayer()
        {
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
        }

        // For use with Activator.CreateInstance
        public SoilLayer(
            int soilLayerId,
            int soilId,
            int? depthFrom,
            int? depthTo
        )
        {
            SoilLayerId = soilLayerId;
            SoilId = soilId;
            DepthFrom = depthFrom;
            DepthTo = depthTo;
        }

        [PrimaryKey]
        [Column("SoilLayerId")]
        public int SoilLayerId { get; set; }

        [Column("SoilId")]
        public int SoilId { get; set; }

        [Nullable]
        [Column("SoilLayerDepthFrom")]
        public int? DepthFrom { get; set; } = null;

        [Nullable]
        [Column("SoilLayerDepthTo")]
        public int? DepthTo { get; set; } = null;


        public virtual Soil Soil { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }
    }
}
