using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Plot")]
    public class Plot
    {
        public Plot()
        {
            PlotData = new HashSet<PlotData>();
            SoilData = new HashSet<SoilData>();
            SoilLayerData = new HashSet<SoilLayerData>();
        }

        public int PlotId { get; set; }
        public int? TreatmentId { get; set; }
        public int? Rep { get; set; }
        public int? Columns { get; set; }
        public int? Rows { get; set; }

        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }
    }
}
