using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class FertilizerConfiguration : IEntityTypeConfiguration<Fertilizer>
    {
        public void Configure(EntityTypeBuilder<Fertilizer> builder)
        {
            builder.HasKey(e => e.FertilizerId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.FertilizerId)
                .HasDatabaseName("FertilizerId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(60);

            builder.Property(e => e.Calcium)
                .HasColumnName("Calcium %");

            builder.Property(e => e.Potassium)
                .HasColumnName("Potassium %");

            builder.Property(e => e.Nitrogen)
                .HasColumnName("Nitrogen %");

            builder.Property(e => e.Phosphorus)
                .HasColumnName("Phosphorus %");

            builder.Property(e => e.Sulfur)
                .HasColumnName("Sulfur %");

            builder.Property(e => e.OtherPercent)
                .HasColumnName("Other %");

            builder.Property(e => e.Other)
                .HasColumnName("Other")
                .HasMaxLength(20);

            builder.Property(e => e.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(100);
        }
    }
}

