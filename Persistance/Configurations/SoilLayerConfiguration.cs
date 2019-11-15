using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class SoilLayerConfiguration : IEntityTypeConfiguration<SoilLayer>
    {
        public void Configure(EntityTypeBuilder<SoilLayer> builder)
        {
            builder.HasKey(e => e.SoilLayerId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.SoilLayerId)
                .HasName("SoilLayerId")
                .IsUnique();

            builder.Property(e => e.SoilId).HasDefaultValueSql("0");

            builder.Property(e => e.DepthFrom).HasDefaultValueSql("0");

            builder.Property(e => e.DepthTo).HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.Soil)
                .WithMany(p => p.SoilLayers)
                .HasForeignKey(d => d.SoilId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilsSoilDepth");
        }
    }
}
