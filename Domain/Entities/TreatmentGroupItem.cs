using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    public class TreatmentGroupItem : IEntity
    {
        public int TreatmentGroupId { get; set; }
        public int TreatmentId { get; set; }

        public Treatment Treatment { get; set; }
        public TreatmentGroup TreatmentGroup { get; set; }

    }
}
