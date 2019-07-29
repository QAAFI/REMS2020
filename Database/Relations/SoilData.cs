using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("SoilData")]
    public class SoilData
    {
        public int SoilDataId { get; set; }
        public int? PlotId { get; set; }
        public int? TraitId { get; set; }
        public DateTime? Date { get; set; }
        public double? Value { get; set; }

        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
