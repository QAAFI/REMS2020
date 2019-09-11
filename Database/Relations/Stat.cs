using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("Stat")]
    public class Stat
    {
        [PrimaryKey]
        [Column("StatId")]
        public int StatId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }

        [Nullable]
        [Column("Date")]
        public DateTime? Date { get; set; }

        [Nullable]
        [Column("Mean")]
        public double? Mean { get; set; }

        [Nullable]
        [Column("SE")]
        public double? SE { get; set; }

        [Nullable]
        [Column("N")]
        public int? Number { get; set; }
        

        public virtual Trait Trait { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stat>(entity =>
            {
                entity.HasIndex(e => e.StatId)
                    .HasName("StatsID");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitStats");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsStats");

                entity.HasIndex(e => e.UnitId)
                    .HasName("UnitsStats");

                entity.Property(e => e.StatId).HasColumnName("StatsID");

                entity.Property(e => e.Mean).HasDefaultValueSql("0");

                entity.Property(e => e.Number)
                    .HasColumnName("n")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SE)
                    .HasColumnName("SE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitStats");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsStats");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UnitsStats");
            });

        }
    }
}
