using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Method")]
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

        [PrimaryKey]
        [Column("MethodId")]
        public int MethodId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Type")]
        public string Type { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }


        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<Harvest> Harvests { get; set; }
        public virtual ICollection<Irrigation> Irrigations { get; set; }
        public virtual ICollection<Tillage> Tillages { get; set; }
    }
}
