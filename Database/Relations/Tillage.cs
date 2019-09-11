using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
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


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tillage>(entity =>
            {
                entity.HasIndex(e => e.TillageId)
                    .HasName("TillageID");

                entity.HasIndex(e => e.MethodId)
                    .HasName("TILLAGE IMPLEMENT_CODE");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsTillage");

                entity.Property(e => e.TillageId).HasColumnName("TillageID");

                entity.Property(e => e.Notes).HasMaxLength(50);

                entity.Property(e => e.MethodId).HasColumnName("TillageMethodID");

                entity.Property(e => e.TreatmentId).HasColumnName("TreatmentID");

                entity.HasOne(d => d.TillageMethod)
                    .WithMany(p => p.Tillages)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MethodTillage");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Tillages)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsTillage");
            });

        }
    }
}
