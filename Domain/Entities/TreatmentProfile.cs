using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    public class TreatmentProfile : IEntity
    {
        public int TreatmentProfileId { get; set; }
        public string Name { get; set; }
        public int ExperimentId { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual TreatmentView TreatmentView { get; set; }
        public virtual ICollection<TreatmentGroup> TreatmentGroups { get; set; }

    }
}
