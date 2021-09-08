﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class SoilDataConfiguration : IEntityTypeConfiguration<SoilData>
    {
        public void Configure(EntityTypeBuilder<SoilData> builder)
        {
            builder.HasKey(e => e.SoilDataId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.SoilDataId)
                .HasDatabaseName("SoilDataId")
                .IsUnique();

            builder.Property(e => e.SoilDataId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.PlotId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.TraitId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Value)
                .HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.Plot)
                .WithMany(p => p.SoilData)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilDataPlotId");

            builder.HasOne(d => d.Trait)
                .WithMany(p => p.SoilData)
                .HasForeignKey(d => d.TraitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SoilDataTraitId");
        }
    }
}
