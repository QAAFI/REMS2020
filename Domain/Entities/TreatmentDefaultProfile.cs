using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    class TreatmentDefaultProfile : IEntity
    {
        public int TreatmentProfileId { get; set; }
        public int TreatmentViewId { get; set; }
    }
}
