using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("ExperimentInfo")]
    public class ExperimentInfo
    {
        // For use with Activator.CreateInstance
        public ExperimentInfo(
            int experimentInfoId,
            int experimentId,
            string infoType,
            string variable,
            string value
        )
        {
            ExperimentInfoId = experimentInfoId;
            ExperimentId = experimentId;
            InfoType = infoType;
            Variable = variable;
            Value = value;
        }

        [PrimaryKey]
        [Column("ExperimentInfoId")]
        public int ExperimentInfoId { get; set; }

        [Column("ExperimentId")]
        public int ExperimentId { get; set; }

        [Nullable]
        [Column("InfoType")]
        public string InfoType { get; set; }

        [Nullable]
        [Column("Variable")]
        public string Variable { get; set; }

        [Nullable]
        [Column("Value")]
        public string Value { get; set; }


        public virtual Experiment Experiment { get; set; }
    }
}
