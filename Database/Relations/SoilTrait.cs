using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("SoilTrait")]
    public class SoilTrait
    {
        [PrimaryKey]
        [Column("SoilTraitId")]
        public int SoilTraitId { get; set; }

        [Column("SoilId")]
        public int? SoilId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Column("Value")]
        public double? Value { get; set; }


        public virtual Soil Soil { get; set; }
        public virtual Trait Trait { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilTrait>(entity =>
            {
                entity.HasKey(e => e.SoilTraitId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilsSoilTraits");

                entity.HasIndex(e => e.SoilTraitId)
                    .HasName("SoilTraitID");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitsSoilTraits");

                entity.Property(e => e.SoilTraitId)
                    .HasColumnName("SoilTraitID");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.SoilTraits)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilsSoilTraits");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilTraits)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitsSoilTraits");
            });

        }
    }
}
