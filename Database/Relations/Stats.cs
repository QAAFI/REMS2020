using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Stats")]
    public class Stats
    {
        [PrimaryKey]
        [Column("StatsId")]
        public int StatsId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }

        [Nullable]
        [Column("Date")]
        public DateTime? Date { get; set; }

        [Nullable]
        [Column("Mean")]
        public double? Mean { get; set; }

        [Nullable]
        [Column("SE")]
        public double? SE { get; set; }

        [Nullable]
        [Column("N")]
        public int? Number { get; set; }
        

        public virtual Trait Trait { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
