using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class ExperimentInfoConfiguration : IEntityTypeConfiguration<ExperimentInfo>
    {
        public void Configure(EntityTypeBuilder<ExperimentInfo> builder)
        {
            builder.HasKey(e => e.ExperimentInfoId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.ExperimentInfoId)
                .HasDatabaseName("ExperimentInfoId")
                .IsUnique();

            builder.Property(e => e.ExperimentInfoId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ExperimentId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.InfoType)
                .HasMaxLength(1);

            builder.Property(e => e.Value)
                .HasMaxLength(20);

            builder.Property(e => e.Variable)
                .HasMaxLength(20);

            // Define the foreign key constraints
            builder.HasOne(d => d.Experiment)
                .WithMany(p => p.ExperimentInfo)
                .HasForeignKey(d => d.ExperimentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ExperimentInfoExperimentId");

        }
    }
}
