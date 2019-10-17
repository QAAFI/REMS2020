using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
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


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stat>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.StatId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.StatId)
                    .HasName("StatId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("StatTraitId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("StatTreatmentId");

                entity.HasIndex(e => e.UnitId)
                    .HasName("StatUnitId");

                // Define properties
                entity.Property(e => e.StatId)
                    .HasColumnName("StatsId");

                entity.Property(e => e.Mean)
                    .HasColumnName("Mean")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Number)
                    .HasColumnName("Number")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SE)
                    .HasColumnName("SE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitId")
                    .HasDefaultValueSql("0");

                // Define constraints
                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("StatTraitId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("StatTreatmentId");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("StatUnitId");
            });

        }
    }
}
