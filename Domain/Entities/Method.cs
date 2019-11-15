using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Method
    {
        public Method()
        {
            ChemicalApplications = new HashSet<ChemicalApplication>();
            Experiments = new HashSet<Experiment>();
            Fertilizations = new HashSet<Fertilization>();
            Harvests = new HashSet<Harvest>();
            Irrigations = new HashSet<Irrigation>();
            Tillages = new HashSet<Tillage>();
        }

        public int MethodId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<Harvest> Harvests { get; set; }
        public virtual ICollection<Irrigation> Irrigations { get; set; }
        public virtual ICollection<Tillage> Tillages { get; set; }

    }
}
