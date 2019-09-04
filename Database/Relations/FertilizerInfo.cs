using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("FertilizerInfo")]
    public class FertilizationInfo
    {
        [PrimaryKey]
        [Column("FertilizationInfoId")]
        public int FertilizationInfoId { get; set; }

        [Column("FertilizationId")]
        public int? FertilizationId { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual Fertilization Fertilization { get; set; }
    }
}
