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

        [Column("FertilizationId")]
        public int FertilizationId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("FertilizationDate")]
        public DateTime? FertilizationDate { get; set; }

        [Column("FertilizerId")]
        public int? FertilizerId { get; set; }

        [Column("FertilizationAmount")]
        public int? FertilizationAmount { get; set; }

        [Column("FertilizationDepth")]
        public int? FertilizationDepth { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }


        public virtual Fertilizer Fertilizer { get; set; }
        public virtual Method Method { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<FertilizationInfo> FertilizationInfo { get; set; }
    }
}
