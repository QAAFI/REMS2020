using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class TraitConfiguration : IEntityTypeConfiguration<Trait>
    {
        public void Configure(EntityTypeBuilder<Trait> builder)
        {
            builder.HasKey(e => e.TraitId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.TraitId)
                .HasDatabaseName("TraitId")
                .IsUnique();

            builder.Property(e => e.TraitId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Description)
                .HasMaxLength(60);

            builder.Property(e => e.Name)
                .HasMaxLength(25);

            builder.Property(e => e.Type)
                .HasMaxLength(10);

            // Define constraints
            builder.HasOne(d => d.Unit)
                .WithMany(p => p.Traits)
                .HasForeignKey(d => d.UnitId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TraitUnitId");
        }
    }
}

