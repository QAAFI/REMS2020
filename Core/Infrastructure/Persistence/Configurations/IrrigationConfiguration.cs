using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class IrrigationConfiguration : IEntityTypeConfiguration<Irrigation>
    {
        public void Configure(EntityTypeBuilder<Irrigation> builder)
        {
            builder.HasKey(e => e.IrrigationId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.IrrigationId)
                .HasDatabaseName("IrrigationId")
                .IsUnique();

            builder.Property(e => e.IrrigationId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Amount)
                .HasColumnName("Amount")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(50);

            builder.Property(e => e.TreatmentId)
                .HasColumnName("TreatmentId");

            // Define foreign key constraints
            builder.HasOne(d => d.Method)
                .WithMany(p => p.Irrigations)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IrrigationMethodId");

            builder.HasOne(d => d.Treatment)
                .WithMany(p => p.Irrigations)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IrrigationTreatmentId");
        }
    }
}

