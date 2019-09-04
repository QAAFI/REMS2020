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

        // For use with Activator.CreateInstance
        public Soil(
            int soilId,
            string soilType,
            string notes
        )
        {
            SoilId = soilId;
            SoilType = soilType;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("SoilId")]
        public int SoilId { get; set; }

        [Nullable]
        [Column("SoilType")]
        public string SoilType { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }
    }
}
