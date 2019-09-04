using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("SoilTrait")]
    public class SoilTrait
    {
        [PrimaryKey]
        [Column("SoilTraitId")]
        public int SoilTraitId { get; set; }

        [Column("SoilId")]
        public int? SoilId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Column("Value")]
        public double? Value { get; set; }


        public virtual Soil Soil { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
