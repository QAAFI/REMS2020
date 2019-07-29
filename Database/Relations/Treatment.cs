using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Treatment")]
    public class Treatment
    {
        public Treatment()
        {
            ChemicalApplications = new HashSet<ChemicalApplication>();
            Designs = new HashSet<Design>();
            Fertilizations = new HashSet<Fertilization>();
            Harvests = new HashSet<Harvest>();
            Irrigations = new HashSet<Irrigation>();
            Plots = new HashSet<Plot>();
            Stats = new HashSet<Stats>();
            Tillages = new HashSet<Tillage>();
        }

        public int TreatmentId { get; set; }
        public int? ExperimentId { get; set; }
        public string TreatmentName { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<Harvest> Harvests { get; set; }
        public virtual ICollection<Irrigation> Irrigations { get; set; }
        public virtual ICollection<Plot> Plots { get; set; }
        public virtual ICollection<Stats> Stats { get; set; }
        public virtual ICollection<Tillage> Tillages { get; set; }
    }
}
