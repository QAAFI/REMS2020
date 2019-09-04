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

        [PrimaryKey]
        [Column("PlotId")]
        public int PlotId { get; set; }

        [Column("TreatmentId")]
        public int TreatmentId { get; set; }

        [Nullable]
        [Column("Repetitions")]
        public int? Repetitions { get; set; }

        [Nullable]
        [Column("Columns")]
        public int? Columns { get; set; }

        [Nullable]
        [Column("Rows")]
        public int? Rows { get; set; }


        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }
    }
}
