using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class ChemicalApplicationConfiguration : IEntityTypeConfiguration<ChemicalApplication>
    {
        public void Configure(EntityTypeBuilder<ChemicalApplication> builder)
        {
            builder.HasKey(e => e.ApplicationId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.ApplicationId)
                .HasName("ApplicationId")
                .IsUnique();

            builder.Property(e => e.Amount).HasDefaultValueSql("0");
            builder.Property(e => e.Notes).HasMaxLength(50);
            builder.Property(e => e.Target).HasMaxLength(25);
            
            builder.HasOne(e => e.ApplicationMethod)
                .WithMany(p => p.ChemicalApplications)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApplicationMethodId");

            builder.HasOne(e => e.Treatment)
                .WithMany(p => p.ChemicalApplications)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApplicationTreatmentId");

            builder.HasOne(e => e.Unit)
                .WithMany(p => p.ChemicalApplications)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApplicationUnitId");
        }
    }
}
