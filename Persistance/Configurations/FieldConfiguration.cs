using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(e => e.FieldId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.FieldId)
                .HasName("FieldId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(20);

            builder.Property(e => e.Slope)
                .HasColumnName("Slope")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Depth)
                .HasColumnName("Depth")
                .HasDefaultValueSql("0");

            // Define foreign key constraints
            builder.HasOne(d => d.Site)
                .WithMany(p => p.Fields)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FieldSiteId");

            builder.HasOne(d => d.Soil)
                .WithMany(p => p.Fields)
                .HasForeignKey(d => d.SoilId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FieldSoilId");
        }
    }
}

