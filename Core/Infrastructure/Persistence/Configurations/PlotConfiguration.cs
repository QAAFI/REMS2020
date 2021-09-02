﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class PlotConfiguration : IEntityTypeConfiguration<Plot>
    {
        public void Configure(EntityTypeBuilder<Plot> builder)
        {
            builder.HasKey(e => e.PlotId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.PlotId)
                .HasDatabaseName("PlotId")
                .IsUnique();

            builder.Property(e => e.Column)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Rows)
                .HasDefaultValueSql("0");

            builder.Property(e => e.TreatmentId)
                .HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.Treatment)
                .WithMany(p => p.Plots)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TreatmentsPlots");
        }
    }
}

