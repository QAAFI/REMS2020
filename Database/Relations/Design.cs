using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Design")]
    public class Design
    {
        public int DesignId { get; set; }
        public int? LevelId { get; set; }
        public int? TreatmentId { get; set; }

        public virtual Level Level { get; set; }
        public virtual Treatment Treatment { get; set; }
    }
}
