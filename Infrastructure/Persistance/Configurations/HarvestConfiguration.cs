using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class HarvestConfiguration : IEntityTypeConfiguration<Harvest>
    {
        public void Configure(EntityTypeBuilder<Harvest> builder)
        {
            builder.HasKey(e => e.HarvestId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.HarvestId)
                .HasName("HarvestId")
                .IsUnique();

            builder.Property(e => e.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(50);

            // Define foreign key constraints
            builder.HasOne(d => d.HarvestMethod)
                .WithMany(p => p.Harvests)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("HarvestMethodId");

            builder.HasOne(d => d.Treatment)
                .WithMany(p => p.Harvests)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("HarvestTreatmentId");
        }
    }
}
