using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("ExperimentInfo")]
    public class ExperimentInfo
    {
        [Column("ExperimentInfoId")]
        public int ExperimentInfoId { get; set; }

        [Column("ExperimentId")]
        public int? ExperimentId { get; set; }

        [Column("InfoType")]
        public string InfoType { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual Experiment Experiment { get; set; }
    }
}
