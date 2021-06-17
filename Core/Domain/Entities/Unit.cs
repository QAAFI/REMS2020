using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Information, "Units")]
    public class Unit : IEntity
    {
        public Unit()
        {
            ChemicalApplications = new HashSet<ChemicalApplication>();
            Fertilizations = new HashSet<Fertilization>();
            PlotData = new HashSet<PlotData>();
            Stats = new HashSet<Stat>();
            Traits = new HashSet<Trait>();
        }

        public int UnitId { get; set; }

        [Expected("Unit", "Units", "Name", "UnitName")]
        public string Name { get; set; } = null;

        [Expected("Notes")]
        public string Notes { get; set; } = null;

        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
        public virtual ICollection<Trait> Traits { get; set; }
    }
}
