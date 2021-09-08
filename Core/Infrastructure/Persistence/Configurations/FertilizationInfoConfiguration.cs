using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class FertilizationInfoConfiguration : IEntityTypeConfiguration<FertilizationInfo>
    {
        public void Configure(EntityTypeBuilder<FertilizationInfo> builder)
        {
            builder.HasKey(e => e.FertilizationInfoId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.FertilizationInfoId)
                .HasDatabaseName("FertilizationInfoId")
                .IsUnique();

            builder.Property(e => e.Value)
                .HasColumnName("Value")
                .HasMaxLength(20);

            builder.Property(e => e.Variable)
                .HasColumnName("Variable")
                .HasMaxLength(20);

            // Define foreign key constraints
            builder.HasOne(d => d.Fertilization)
                .WithMany(p => p.FertilizationInfo)
                .HasForeignKey(d => d.FertilizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FertilizationInfoFertilizationId");
        }
    }
}
