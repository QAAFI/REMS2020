using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class SoilTraitConfiguration : IEntityTypeConfiguration<SoilTrait>
    {
        public void Configure(EntityTypeBuilder<SoilTrait> builder)
        {
            builder.HasKey(e => e.SoilTraitId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.SoilTraitId)
                .HasName("SoilTraitId")
                .IsUnique();

            builder.Property(e => e.SoilId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.TraitId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Value)
                .HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.Soil)
                .WithMany(p => p.SoilTraits)
                .HasForeignKey(d => d.SoilId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilsSoilTraits");

            builder.HasOne(d => d.Trait)
                .WithMany(p => p.SoilTraits)
                .HasForeignKey(d => d.TraitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TraitsSoilTraits");
        }
    }
}
