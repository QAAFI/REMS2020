using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("TillageInfo")]
    public class TillageInfo
    {
        [PrimaryKey]
        [Column("TillageInfoId")]
        public int TillageInfoId { get; set; }

        [Column("TillageId")]
        public int? TillageId { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual Tillage Tillage { get; set; }
    }
}
