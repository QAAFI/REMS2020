using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Soil")]
    public class Soil
    {
        public Soil()
        {
            Fields = new HashSet<Field>();
            SoilLayers = new HashSet<SoilLayer>();
            SoilTraits = new HashSet<SoilTrait>();
        }

        public int SoilId { get; set; }
        public string SoilType { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }
    }
}
