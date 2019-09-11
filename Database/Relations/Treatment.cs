using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

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
            Stats = new HashSet<Stat>();
            Tillages = new HashSet<Tillage>();
        }

        [PrimaryKey]
        [Column("TreatmentId")]
        public int TreatmentId { get; set; }

        [Column("ExperimentId")]
        public int? ExperimentId { get; set; }

        [Column("TreatmentName")]
        public string TreatmentName { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<Harvest> Harvests { get; set; }
        public virtual ICollection<Irrigation> Irrigations { get; set; }
        public virtual ICollection<Plot> Plots { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
        public virtual ICollection<Tillage> Tillages { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.HasKey(e => e.TreatmentId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.ExperimentId)
                    .HasName("ExpID");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentID");

                entity.Property(e => e.TreatmentId).HasColumnName("TreatmentID");

                entity.Property(e => e.ExperimentId)
                    .HasColumnName("ExpID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TreatmentName).HasMaxLength(50);

                entity.HasOne(d => d.Experiment)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.ExperimentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentsTreatments");
            });

        }
    }
}
