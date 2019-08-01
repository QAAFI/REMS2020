using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Design")]
    public class Design
    {
        [Column("DesignId")]
        public int DesignId { get; set; }

        [Column("LevelId")]
        public int? LevelId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }


        public virtual Level Level { get; set; }
        public virtual Treatment Treatment { get; set; }
    }
}
