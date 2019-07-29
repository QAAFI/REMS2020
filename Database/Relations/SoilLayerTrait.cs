using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("SoilLayerTrait")]
    public class SoilLayerTrait
    {
        public int SoilLayerTraitId { get; set; }
        public int? SoilLayerId { get; set; }
        public int? TraitId { get; set; }
        public double? Value { get; set; }

        public virtual SoilLayer SoilLayer { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
