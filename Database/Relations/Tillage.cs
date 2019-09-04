using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Tillage")]
    public class Tillage
    {
        public Tillage()
        {
            TillageInfo = new HashSet<TillageInfo>();
        }

        [PrimaryKey]
        [Column("TillageId")]
        public int TillageId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("Date")]
        public DateTime? Date { get; set; }        

        [Column("Depth")]
        public double? Depth { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        public virtual Method TillageMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<TillageInfo> TillageInfo { get; set; }
    }
}
