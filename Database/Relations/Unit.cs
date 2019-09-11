using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Unit")]
    public class Unit
    {
        public Unit()
        {
            ChemicalApplications = new HashSet<ChemicalApplication>();
            Fertilizations = new HashSet<Fertilization>();
            PlotData = new HashSet<PlotData>();
            Stats = new HashSet<Stats>();
            Traits = new HashSet<Trait>();
        }

        // For use with Activator.CreateInstance
        public Unit(
            double unitId,
            string unitName,
            string notes
        )
        {
            UnitId = (int)unitId;
            UnitName = unitName;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("UnitId")]
        public int UnitId { get; set; }

        [Nullable]
        [Column("UnitName")]
        public string UnitName { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;

        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<Stats> Stats { get; set; }
        public virtual ICollection<Trait> Traits { get; set; }
    }
}
