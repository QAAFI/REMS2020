using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("Design")]
    public class Design
    {
        public Design()
        { }

        // For use with Activator.CreateInstance()
        public Design(
            int designId,
            int? levelId,
            int? treatmentId
        )
        {
            DesignId = designId;
            LevelId = levelId;
            TreatmentId = treatmentId;
        }

        [PrimaryKey]
        [Column("DesignId")]
        public int DesignId { get; set; }

        [Column("LevelId")]
        public int? LevelId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [ForeignKey]
        public virtual Level Level { get; set; }

        [ForeignKey]
        public virtual Treatment Treatment { get; set; }

        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Design>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.DesignId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.DesignId)
                    .HasName("DesignId");

                entity.HasIndex(e => e.LevelId)
                    .HasName("DesignLevelId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("DesignTreatmentId");

                // Define the table properties
                entity.Property(e => e.DesignId)
                    .HasColumnName("DesignId");

                entity.Property(e => e.LevelId)
                    .HasColumnName("LevelId");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId");

                // Define foreign key constraints
                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Designs)
                    .HasForeignKey(d => d.LevelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("DesignLevelId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Designs)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("DesignTreatmentId");
            });

        }
    }
}
