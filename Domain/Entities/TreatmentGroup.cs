using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    class TreatmentGroup : IEntity
    {
        public int TreatmentGroupId { get; set; }
        public string Name { get; set; }
        public int TreatmentProfileId { get; set; }

    }
}
