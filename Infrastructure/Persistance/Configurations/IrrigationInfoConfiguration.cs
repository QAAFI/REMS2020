using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class IrrigationInfoConfiguration : IEntityTypeConfiguration<IrrigationInfo>
    {
        public void Configure(EntityTypeBuilder<IrrigationInfo> builder)
        {
            builder.HasKey(e => e.IrrigationInfoId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.IrrigationInfoId)
                .HasName("IrrigationInfoId")
                .IsUnique();

            builder.Property(e => e.IrrigationId)
                .HasColumnName("IrrigID")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Value)
                .HasColumnName("Value")
                .HasMaxLength(20);

            builder.Property(e => e.Variable)
                .HasColumnName("Variable")
                .HasMaxLength(20);

            // Define foreign key constraints
            builder.HasOne(d => d.Irrigation)
                .WithMany(p => p.IrrigationInfo)
                .HasForeignKey(d => d.IrrigationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IrrigationInfoIrrigationId");
        }
    }
}
