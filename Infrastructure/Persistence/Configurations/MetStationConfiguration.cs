using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class MetStationConfiguration : IEntityTypeConfiguration<MetStation>
    {
        public void Configure(EntityTypeBuilder<MetStation> builder)
        {
            builder.HasKey(e => e.MetStationId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.MetStationId)
                .HasDatabaseName("MetStationId")
                .IsUnique();

            builder.Property(e => e.Amplitude)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Name)
                .HasMaxLength(40);

            builder.Property(e => e.TemperatureAverage)
                .HasDefaultValueSql("0");
        }
    }
}

