using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class SoilLayerTraitConfiguration : IEntityTypeConfiguration<SoilLayerTrait>
    {
        public void Configure(EntityTypeBuilder<SoilLayerTrait> builder)
        {
            builder.HasKey(e => e.SoilLayerTraitId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.SoilLayerTraitId)
                .HasDatabaseName("SoilLayerTraitId")
                .IsUnique();

            builder.Property(e => e.SoilLayerTraitId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SoilLayerId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.TraitId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Value)
                .HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.SoilLayer)
                .WithMany(p => p.SoilLayerTraits)
                .HasForeignKey(d => d.SoilLayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilLayerTraitSoilLayerId");

            builder.HasOne(d => d.Trait)
                .WithMany(p => p.SoilLayerTraits)
                .HasForeignKey(d => d.TraitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilLayerTraitTraitId");
        }
    }
}
