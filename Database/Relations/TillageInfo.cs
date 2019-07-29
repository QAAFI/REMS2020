using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("TillageInfo")]
    public class TillageInfo
    {
        public int TillageInfoId { get; set; }
        public int? TillageId { get; set; }
        public string Variable { get; set; }
        public string Value { get; set; }

        public virtual Tillage Tillage { get; set; }
    }
}
