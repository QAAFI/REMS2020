using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Harvest")]
    public class Harvest
    {
        public int? TreatmentId { get; set; }
        public int HarvestId { get; set; }
        public DateTime? HarvestDate { get; set; }
        public int? MethodId { get; set; }
        public string Notes { get; set; }

        public virtual Method HarvestMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
    }
}
