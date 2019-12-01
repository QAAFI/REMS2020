using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    class TreatmentGroupItem : IEntity
    {
        public int TreatmentGroupId { get; set; }
        public int TreatmentId { get; set; }

    }
}
