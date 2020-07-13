using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Treatment : INamed
    {
        public Treatment()
        {
            ChemicalApplications = new HashSet<ChemicalApplication>();
            Designs = new HashSet<Design>();
            Fertilizations = new HashSet<Fertilization>();
            Harvests = new HashSet<Harvest>();
            Irrigations = new HashSet<Irrigation>();
            Plots = new HashSet<Plot>();
            Stats = new HashSet<Stat>();
            Tillages = new HashSet<Tillage>();
        }

        public int TreatmentId { get; set; }

        public int? ExperimentId { get; set; }

        public string Name { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual Sowing Sowing { get; set; }
        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<Harvest> Harvests { get; set; }
        public virtual ICollection<Irrigation> Irrigations { get; set; }
        public virtual ICollection<Plot> Plots { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
        public virtual ICollection<Tillage> Tillages { get; set; }

    }
}
