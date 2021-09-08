using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class SowingConfiguration : IEntityTypeConfiguration<Sowing>
    {
        public void Configure(EntityTypeBuilder<Sowing> builder)
        {
            builder.HasKey(e => e.SowingId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.SowingId)
                .HasDatabaseName("SowingId")
                .IsUnique();

            builder.Property(e => e.Cultivar)
                .HasMaxLength(30);

            builder.Property(e => e.Notes)
                .HasMaxLength(50);

            // Define foreign key constraints
            builder.HasOne(d => d.Method)
                .WithMany(p => p.Sowings)
                .HasForeignKey(p => p.MethodId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Experiment)
                .WithOne(p => p.Sowing)
                .HasForeignKey<Sowing>(p => p.ExperimentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
