using System;
using System.Collections.Generic;

namespace Database
{
    public partial class SoilTraits
    {
        public int SoilTraitId { get; set; }
        public int? SoilId { get; set; }
        public int? TraitId { get; set; }
        public double? Value { get; set; }

        public virtual Soil Soil { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
