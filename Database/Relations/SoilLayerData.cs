using System;
using System.Collections.Generic;

namespace Database
{
    public partial class SoilLayerData
    {
        public int SoilLayerId { get; set; }
        public int? PlotId { get; set; }
        public int? TraitId { get; set; }
        public DateTime? Date { get; set; }
        public int? SoilLayerDataDepthFrom { get; set; }
        public int? SoilLayerDataDepthTo { get; set; }
        public double? Value { get; set; }

        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
