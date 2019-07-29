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

        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<Stats> Stats { get; set; }
        public virtual ICollection<Trait> Traits { get; set; }
    }
}
