using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class SoilLayerTrait : BaseEntity
    {
        public SoilLayerTrait() : base()
        { }

        public int SoilLayerTraitId { get; set; }

        public int? SoilLayerId { get; set; }

        public int? TraitId { get; set; }

        public double? Value { get; set; }


        public virtual SoilLayer SoilLayer { get; set; }
        public virtual Trait Trait { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilLayerTrait>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.SoilLayerTraitId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.SoilLayerId)
                    .HasName("SoilLayerTraitSoilLayerId");

                entity.HasIndex(e => e.SoilLayerTraitId)
                    .HasName("SoilLayerTraitId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("SoilLayerTraitTraitId");

                // Define properties
                entity.Property(e => e.SoilLayerTraitId)
                    .HasColumnName("SoilLayerTraitId");

                entity.Property(e => e.SoilLayerId)
                    .HasColumnName("SoilLayerId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasDefaultValueSql("0");

                // Define constraints
                entity.HasOne(d => d.SoilLayer)
                    .WithMany(p => p.SoilLayerTraits)
                    .HasForeignKey(d => d.SoilLayerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilLayerTraitSoilLayerId");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilLayerTraits)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilLayerTraitTraitId");
            });

        }
    }
}
