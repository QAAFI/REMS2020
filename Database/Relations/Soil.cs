using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Soil
    {
        public Soil()
        {
            Fields = new HashSet<Field>();
            SoilLayers = new HashSet<SoilLayer>();
            SoilTraits = new HashSet<SoilTraits>();
        }

        public int SoilId { get; set; }
        public string SoilType { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTraits> SoilTraits { get; set; }
    }
}
