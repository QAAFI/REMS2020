using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("MetInfo")]
    public class MetInfo
    {
        [PrimaryKey]
        [Column("MetInfoId")]
        public int MetInfoId { get; set; }

        [Column("MetStationId")]
        public int? MetStationId { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual MetStations MetStation { get; set; }
    }
}
