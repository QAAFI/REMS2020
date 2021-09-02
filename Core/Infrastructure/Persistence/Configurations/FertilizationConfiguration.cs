using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class FertilizationConfiguration : IEntityTypeConfiguration<Fertilization>
    {
        public void Configure(EntityTypeBuilder<Fertilization> builder)
        {
            builder.HasKey(e => e.FertilizationId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.FertilizationId)
                .HasDatabaseName("FertilizationId")
                .IsUnique();

            builder.Property(e => e.FertilizationId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Amount)
                .HasDefaultValueSql("0");
            
            builder.Property(e => e.Depth)
                .HasDefaultValueSql("0");
            
            builder.Property(e => e.TreatmentId)
                .HasDefaultValueSql("0");
            
            builder.Property(e => e.Notes)
                .HasMaxLength(100);

            // Define foreign key constraints
            builder.HasOne(d => d.Fertilizer)
                .WithMany(p => p.Fertilization)
                .HasForeignKey(d => d.FertilizerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FertilizationFertilizerId");

            builder.HasOne(d => d.Method)
                .WithMany(p => p.Fertilizations)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FertilizationMethodId");

            builder.HasOne(d => d.Treatment)
                .WithMany(p => p.Fertilizations)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FertilizationTreatmentId");

            builder.HasOne(d => d.Unit)
                .WithMany(p => p.Fertilizations)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FertilizationUnitId");
        }
    }
}
