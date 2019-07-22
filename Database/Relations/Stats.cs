using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Stats
    {
        public int StatsId { get; set; }
        public int? TreatmentId { get; set; }
        public int? TraitId { get; set; }
        public DateTime? StatsDate { get; set; }
        public double? Mean { get; set; }
        public double? Se { get; set; }
        public int? N { get; set; }
        public int? UnitId { get; set; }

        public virtual Trait Trait { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
