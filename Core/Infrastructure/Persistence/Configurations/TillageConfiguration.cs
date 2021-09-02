using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class TillageConfiguration : IEntityTypeConfiguration<Tillage>
    {
        public void Configure(EntityTypeBuilder<Tillage> builder)
        {
            builder.HasKey(e => e.TillageId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.TillageId)
                .HasDatabaseName("TillageId")
                .IsUnique();

            builder.Property(e => e.TillageId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Notes)
                .HasMaxLength(50);

            // Define constraints
            builder.HasOne(d => d.Method)
                .WithMany(p => p.Tillages)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TillageMethodId");

            builder.HasOne(d => d.Treatment)
                .WithMany(p => p.Tillages)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TillageTreatmentId");
        }
    }
}
