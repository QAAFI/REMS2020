using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource("Information", "Soils")]
    public class Soil : IEntity
    {
        public Soil()
        {
            Fields = new HashSet<Field>();
            SoilLayers = new HashSet<SoilLayer>();
            SoilTraits = new HashSet<SoilTrait>();
        }

        public int SoilId { get; set; }

        [Expected("SoilType", "Type")]
        public string SoilType { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }
    }
}
