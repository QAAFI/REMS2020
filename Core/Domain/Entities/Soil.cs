using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Soil : IEntity
    {
        public Soil()
        {
            Fields = new HashSet<Field>();
            SoilLayers = new HashSet<SoilLayer>();
            SoilTraits = new HashSet<SoilTrait>();
        }

        public int SoilId { get; set; }

        public string Type { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }
    }
}
