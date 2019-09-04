using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("SoilData")]
    public class SoilData
    {
        public SoilData()
        { }

        // For use with Activator.CreateInstance
        public SoilData(
            int soilDataId,
            int plotId,
            int traitId,
            DateTime soilDataDate,
            double value
        )
        {
            SoilDataId = soilDataId;
            PlotId = plotId;
            TraitId = traitId;
            SoilDataDate = soilDataDate;
            Value = value;
        }

        [PrimaryKey]
        [Column("SoilDataId")]
        public int SoilDataId { get; set; }

        [Column("PlotId")]
        public int PlotId { get; set; }

        [Column("TraitId")]
        public int TraitId { get; set; }

        [Nullable]
        [Column("Date")]
        public DateTime? SoilDataDate { get; set; }

        [Nullable]
        [Column("Value")]
        public double? Value { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
