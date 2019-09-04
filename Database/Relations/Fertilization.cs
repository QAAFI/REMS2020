using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Fertilization")]
    public class Fertilization
    {
        public Fertilization()
        {
            FertilizationInfo = new HashSet<FertilizationInfo>();
        }

        [PrimaryKey]
        [Column("FertilizationId")]
        public int FertilizationId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("FertilizerId")]
        public int? FertilizerId { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }

        [Nullable]
        [Column("Date")]
        public DateTime? Date { get; set; }        

        [Nullable]
        [Column("Amount")]
        public int? Amount { get; set; }

        [Nullable]
        [Column("Depth")]
        public int? Depth { get; set; }        

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }


        public virtual Fertilizer Fertilizer { get; set; }
        public virtual Method Method { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<FertilizationInfo> FertilizationInfo { get; set; }
    }
}
