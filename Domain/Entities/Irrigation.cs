using System;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Irrigation : IEntity
    {
        public Irrigation()
        {
            IrrigationInfo = new HashSet<IrrigationInfo>();
        }

        public int IrrigationId { get; set; }

        public int? MethodId { get; set; }

        public int? TreatmentId { get; set; }

        public DateTime? Date { get; set; }        

        public int? Amount { get; set; }

        public string Notes { get; set; }


        public virtual Method IrrigationMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<IrrigationInfo> IrrigationInfo { get; set; }

    }
}
