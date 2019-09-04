using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Harvest")]
    public class Harvest
    {
        public Harvest(
            int harvestId,
            int treatmentId,
            int methodId,
            DateTime harvestDate,
            string notes
        )
        {
            HarvestId = harvestId;
            TreatmentId = treatmentId;
            MethodId = methodId;
            HarvestDate = harvestDate;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("HarvestId")]
        public int HarvestId { get; set; }

        [Column("TreatmentId")]
        public int TreatmentId { get; set; }

        [Column("MethodId")]
        public int MethodId { get; set; }

        [Nullable]
        [Column("HarvestDate")]
        public DateTime? HarvestDate { get; set; }        

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }

        public virtual Method HarvestMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
    }
}
