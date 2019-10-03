using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("Harvest")]
    public class Harvest
    {
        public Harvest()
        { }

        public Harvest(
            int harvestId,
            int treatmentId,
            int methodId,
            DateTime harvestDate,
            string notes
        )
        {
            HarvestId = harvestId;
            TreatmentId = treatmentId;
            MethodId = methodId;
            Date = harvestDate;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("HarvestId")]
        public int HarvestId { get; set; }

        [Column("TreatmentId")]
        public int TreatmentId { get; set; }

        [Column("MethodId")]
        public int MethodId { get; set; }

        [Nullable]
        [Column("HarvestDate")]
        public DateTime? Date { get; set; }        

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }

        public virtual Method HarvestMethod { get; set; }
        public virtual Treatment Treatment { get; set; }

        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Harvest>(entity =>
            {
                // Define primary key
                entity.HasKey(e => e.HarvestId)
                    .HasName("PrimaryKey");

                // Define table indices
                entity.HasIndex(e => e.HarvestId)
                    .HasName("HarvestId")
                    .IsUnique();

                entity.HasIndex(e => e.MethodId)
                    .HasName("HarvestMethodId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("HarvestTreatmentId");

                // Define table properties
                entity.Property(e => e.HarvestId)
                    .HasColumnName("HarvestId");

                entity.Property(e => e.Date)
                    .HasColumnName("Date");

                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodId");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId");

                entity.Property(e => e.Notes)
                    .HasColumnName("Notes")
                    .HasMaxLength(50);

                // Define foreign key constraints
                entity.HasOne(d => d.HarvestMethod)
                    .WithMany(p => p.Harvests)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("HarvestMethodId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Harvests)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("HarvestTreatmentId");
            });

        }
    }
}
