using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class TillageInfoConfiguration : IEntityTypeConfiguration<TillageInfo>
    {
        public void Configure(EntityTypeBuilder<TillageInfo> builder)
        {
            builder.HasKey(e => e.TillageInfoId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.TillageInfoId)
                .HasDatabaseName("TillageInfoId")
                .IsUnique();

            builder.Property(e => e.TillageId)
                .HasColumnName("TillageID") //??
                .HasDefaultValueSql("0");

            builder.Property(e => e.Value)
                .HasMaxLength(20);

            builder.Property(e => e.Variable)
                .HasMaxLength(20);

            // Define constraints
            builder.HasOne(d => d.Tillage)
                .WithMany(p => p.TillageInfo)
                .HasForeignKey(d => d.TillageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TillageInfoTillageId");
        }
    }
}

