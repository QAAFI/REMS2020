using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Irrigation
    {
        public Irrigation()
        {
            IrrigationInfo = new HashSet<IrrigationInfo>();
        }

        public int IrrigationId { get; set; }
        public int? TreatmentId { get; set; }
        public DateTime? IrrigationDate { get; set; }
        public int? IrrigationMethodId { get; set; }
        public int? Amount { get; set; }
        public string Notes { get; set; }

        public virtual Method IrrigationMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<IrrigationInfo> IrrigationInfo { get; set; }
    }
}
