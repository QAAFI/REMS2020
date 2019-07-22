using System;
using System.Collections.Generic;

namespace Database
{
    public partial class ExperimentInfo
    {
        public int ExperimentInfoId { get; set; }
        public int? ExperimentId { get; set; }
        public string InfoType { get; set; }
        public string Variable { get; set; }
        public string Value { get; set; }

        public virtual Experiment Experiment { get; set; }
    }
}
