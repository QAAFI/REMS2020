using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class DesignConfiguration : IEntityTypeConfiguration<Design>
    {
        public void Configure(EntityTypeBuilder<Design> builder)
        {
            builder.HasKey(e => e.DesignId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.DesignId)
                .HasDatabaseName("DesignId")
                .IsUnique();

            builder.HasOne(e => e.Level)
                .WithMany(p => p.Designs)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DesignLevelId");


            builder.HasOne(d => d.Treatment)
                .WithMany(p => p.Designs)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DesignTreatmentId");
        }
    }
}
