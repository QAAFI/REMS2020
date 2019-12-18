using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class PlotDataConfiguration : IEntityTypeConfiguration<PlotData>
    {
        public void Configure(EntityTypeBuilder<PlotData> builder)
        {
            builder.HasKey(e => e.PlotDataId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.PlotDataId)
                .HasName("PlotDataId")
                .IsUnique();

            builder.Property(e => e.PlotId).HasDefaultValueSql("0");

            builder.Property(e => e.Sample).HasMaxLength(10);

            builder.Property(e => e.TraitId).HasDefaultValueSql("0");

            builder.Property(e => e.UnitId).HasDefaultValueSql("0");

            builder.Property(e => e.Value).HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.Plot)
                .WithMany(p => p.PlotData)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlotDataPlotId");

            builder.HasOne(d => d.Trait)
                .WithMany(p => p.PlotData)
                .HasForeignKey(d => d.TraitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlotDataTraitId");

            builder.HasOne(d => d.Unit)
                .WithMany(p => p.PlotData)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlotDataUnitId");
        }
    }
}

