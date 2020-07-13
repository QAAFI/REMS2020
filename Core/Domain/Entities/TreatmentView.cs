using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    public class TreatmentView : INamed
    {
        public int TreatmentViewId { get; set; }
        public string Name { get; set; }

        public int ExperimentId { get; set; }
        public int? TreatmentProfileId { get; set; }
        public virtual TreatmentProfile TreatmentProfile { get; set; }
        public virtual Experiment Experiment { get; set; }
    }
}
