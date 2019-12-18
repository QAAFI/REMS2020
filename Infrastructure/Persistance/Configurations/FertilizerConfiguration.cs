using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class FertilizerConfiguration : IEntityTypeConfiguration<Fertilizer>
    {
        public void Configure(EntityTypeBuilder<Fertilizer> builder)
        {
            builder.HasKey(e => e.FertilizerId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.FertilizerId)
                .HasName("FertilizerId")
                .IsUnique();

            builder.Property(e => e.Name).HasMaxLength(20);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(60);

            builder.Property(e => e.Calcium)
                .HasColumnName("Calcium")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Potassium)
                .HasColumnName("Potassium")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Nitrogen)
                .HasColumnName("Nitrogen")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Phosphorus)
                .HasColumnName("Phosphorus")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Sulfur)
                .HasColumnName("Sulfur")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Other)
                .HasColumnName("Other")
                .HasDefaultValueSql("0");

            builder.Property(e => e.OtherElements)
                .HasColumnName("OtherElements")
                .HasMaxLength(20);

            builder.Property(e => e.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(100);
        }
    }
}

