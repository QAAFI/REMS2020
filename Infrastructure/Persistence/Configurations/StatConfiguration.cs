using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class StatConfiguration : IEntityTypeConfiguration<Stat>
    {
        public void Configure(EntityTypeBuilder<Stat> builder)
        {
            builder.HasKey(e => e.StatId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.StatId)
                .HasDatabaseName("StatId")
                .IsUnique();

            // Define properties
            builder.Property(e => e.StatId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TreatmentId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.TraitId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.UnitId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Mean)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Number)
                .HasDefaultValueSql("0");

            builder.Property(e => e.SE)
                .HasDefaultValueSql("0");


            // Define constraints
            builder.HasOne(d => d.Trait)
                .WithMany(p => p.Stats)
                .HasForeignKey(d => d.TraitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("StatTraitId");

            builder.HasOne(d => d.Treatment)
                .WithMany(p => p.Stats)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("StatTreatmentId");

            builder.HasOne(d => d.Unit)
                .WithMany(p => p.Stats)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("StatUnitId");
        }
    }
}
