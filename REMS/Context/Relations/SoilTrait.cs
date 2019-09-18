using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
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

        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilTrait>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.SoilTraitId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilTraitSoilId");

                entity.HasIndex(e => e.SoilTraitId)
                    .HasName("SoilTraitId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("SoilTraitTraitId");

                // Define properties
                entity.Property(e => e.SoilTraitId)
                    .HasColumnName("SoilTraitId");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasDefaultValueSql("0");

                // Define constraints
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
