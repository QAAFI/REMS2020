using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class SoilLayerConfiguration : IEntityTypeConfiguration<SoilLayer>
    {
        public void Configure(EntityTypeBuilder<SoilLayer> builder)
        {
            builder.HasKey(e => e.SoilLayerId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.SoilLayerId)
                .HasDatabaseName("SoilLayerId")
                .IsUnique();

            builder.Property(e => e.SoilLayerId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SoilId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.FromDepth)
                .HasDefaultValueSql("0");

            builder.Property(e => e.ToDepth)
                .HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.Soil)
                .WithMany(p => p.SoilLayers)
                .HasForeignKey(d => d.SoilId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilsSoilDepth");
        }
    }
}
