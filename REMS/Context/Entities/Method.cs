using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Method : BaseEntity
    {
        public Method() : base()
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


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Method>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.MethodId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.MethodId)
                    .HasName("MethodId")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("MethodName");

                // Define the table properties
                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(30);

                entity.Property(e => e.Type)
                    .HasColumnName("Type")
                    .HasMaxLength(15);
            });

        }
    }
}
