using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("IrrigationInfo")]
    public class IrrigationInfo
    {
        [PrimaryKey]
        [Column("IrrigationInfoId")]
        public int IrrigationInfoId { get; set; }

        [Column("IrrigationId")]
        public int? IrrigationId { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual Irrigation Irrigation { get; set; }
    }
}
