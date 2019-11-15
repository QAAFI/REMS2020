using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class MetInfoConfiguration : IEntityTypeConfiguration<MetInfo>
    {
        public void Configure(EntityTypeBuilder<MetInfo> builder)
        {
            builder.HasKey(e => e.MetInfoId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.MetInfoId)
                .HasName("MetInfoId")
                .IsUnique();

            builder.Property(e => e.MetStationId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Value)
                .HasMaxLength(20);

            builder.Property(e => e.Variable)
                .HasMaxLength(20);

            // Define the constraints
            builder.HasOne(d => d.MetStation)
                .WithMany(p => p.MetInfo)
                .HasForeignKey(d => d.MetStationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MetInfoMetStationId");
        }
    }
}

