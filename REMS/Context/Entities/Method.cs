using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
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

        public Method(
            double methodId,
            string name,
            string type,
            string notes
        )
        {
            MethodId = (int)methodId;
            Name = name;
            Type = type;
            Notes = notes;
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


        public static void BuildModel(ModelBuilder modelBuilder)
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
