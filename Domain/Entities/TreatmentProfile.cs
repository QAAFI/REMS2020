using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    class TreatmentProfile : IEntity
    {
        public int TreatmentProfileId { get; set; }
        public string Name { get; set; }
        public int ExperimentId { get; set; }
    }
}
