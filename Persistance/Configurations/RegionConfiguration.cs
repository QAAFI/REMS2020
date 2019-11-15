using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(e => e.RegionId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.RegionId)
                .HasName("RegionId")
                .IsUnique();

            builder.Property(e => e.Name).HasMaxLength(20);
        }
    }
}
