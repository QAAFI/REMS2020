using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    public class TreatmentGroup : INamed
    {
        public int TreatmentGroupId { get; set; }
        public string Name { get; set; }
        public int TreatmentProfileId { get; set; }

        public virtual ICollection<TreatmentGroupItem> TreatmentGroupItems { get; set; }
        public virtual TreatmentProfile TreatmentProfile { get; set; }
    }
}
