using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("Tillage")]
    public class Tillage
    {
        public Tillage()
        {
            TillageInfo = new HashSet<TillageInfo>();
        }

        [PrimaryKey]
        [Column("TillageId")]
        public int TillageId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("Date")]
        public DateTime? Date { get; set; }        

        [Column("Depth")]
        public double? Depth { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        public virtual Method TillageMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<TillageInfo> TillageInfo { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tillage>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.TillageId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.TillageId)
                    .HasName("TillageId");

                entity.HasIndex(e => e.MethodId)
                    .HasName("TillageMethodId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TillageTreatmentId");

                // Define properties
                entity.Property(e => e.TillageId)
                    .HasColumnName("TillageId");

                entity.Property(e => e.Notes)
                    .HasColumnName("Notes")
                    .HasMaxLength(50);

                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodId");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId");

                // Define constraints
                entity.HasOne(d => d.TillageMethod)
                    .WithMany(p => p.Tillages)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TillageMethodId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Tillages)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TillageTreatmentId");
            });

        }
    }
}
