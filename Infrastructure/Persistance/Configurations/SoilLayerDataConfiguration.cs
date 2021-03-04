using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class SoilLayerDataConfiguration : IEntityTypeConfiguration<SoilLayerData>
    {
        public void Configure(EntityTypeBuilder<SoilLayerData> builder)
        {
            builder.HasKey(e => e.SoilLayerDataId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.SoilLayerDataId)
                .HasName("SoilLayerDataId")
                .IsUnique();

            // Define constraints
            builder.HasOne(d => d.Plot)
                .WithMany(p => p.SoilLayerData)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilLayerDataPlotId");

            builder.HasOne(d => d.Trait)
                .WithMany(p => p.SoilLayerData)
                .HasForeignKey(d => d.TraitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilLayerDataTraitId");
        }
    }
}
