using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("SoilLayerData")]
    public class SoilLayerData
    {
        public SoilLayerData()
        { }

        public SoilLayerData(
            int plotId 
        )
        {

        }

        [PrimaryKey]
        [Column("SoilLayerDataId")]
        public int SoilLayerDataId { get; set; }

        [Column("PlotId")]
        public int? PlotId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Nullable]
        [Column("Date")]
        public DateTime? Date { get; set; }

        [Nullable]
        [Column("DepthFrom")]
        public int? DepthFrom { get; set; }

        [Nullable]
        [Column("DepthTo")]
        public int? DepthTo { get; set; }

        [Nullable]
        [Column("Value")]
        public double? Value { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
