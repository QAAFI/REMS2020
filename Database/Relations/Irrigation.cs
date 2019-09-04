using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Irrigation")]
    public class Irrigation
    {
        public Irrigation()
        {
            IrrigationInfo = new HashSet<IrrigationInfo>();
        }

        [PrimaryKey]
        [Column("IrrigationId")]
        public int IrrigationId { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("Date")]
        public DateTime? Date { get; set; }        

        [Column("Amount")]
        public int? Amount { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }


        public virtual Method IrrigationMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<IrrigationInfo> IrrigationInfo { get; set; }
    }
}
