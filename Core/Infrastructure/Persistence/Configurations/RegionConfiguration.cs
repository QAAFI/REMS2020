using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(e => e.RegionId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.RegionId)
                .HasDatabaseName("RegionId")
                .IsUnique();

            builder.Property(e => e.RegionId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(20);            
        }
    }
}
