using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("SoilLayerTrait")]
    public class SoilLayerTrait
    {
        [PrimaryKey]
        [Column("SoilLayerTraitId")]
        public int SoilLayerTraitId { get; set; }

        [Column("SoilLayerId")]
        public int? SoilLayerId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Column("Value")]
        public double? Value { get; set; }


        public virtual SoilLayer SoilLayer { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
