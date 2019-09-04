using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("PlotData")]
    public class PlotData
    {
        [PrimaryKey]
        [Column("PlotDataId")]
        public int PlotDataId { get; set; }

        [Column("PlotId")]
        public int? PlotId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Column("PlotDataDate")]
        public DateTime? PlotDataDate { get; set; }

        [Column("Sample")]
        public string Sample { get; set; }

        [Column("Value")]
        public double? Value { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
