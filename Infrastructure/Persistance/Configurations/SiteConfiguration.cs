using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.HasKey(e => e.SiteId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.SiteId)
                .HasName("SiteId")
                .IsUnique();

            builder.Property(e => e.RegionId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            // Define constraints
            builder.HasOne(d => d.Region)
                .WithMany(p => p.Sites)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SiteRegionId");

        }
    }
}
