using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class SowingConfiguration : IEntityTypeConfiguration<Sowing>
    {
        public void Configure(EntityTypeBuilder<Sowing> builder)
        {
            builder.HasKey(e => e.SowingId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.SowingId)
                .HasName("SowingId")
                .IsUnique();

            builder.Property(e => e.FTN)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Cultivar)
                .HasMaxLength(30);

            builder.Property(e => e.SkipConfig)
                .HasMaxLength(30);

            builder.Property(e => e.Notes)
                .HasMaxLength(50);

            // Define foreign key constraints
            builder.HasOne(d => d.Treatment)
                .WithOne(p => p.Sowing)
                .HasForeignKey<Sowing>(p => p.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
