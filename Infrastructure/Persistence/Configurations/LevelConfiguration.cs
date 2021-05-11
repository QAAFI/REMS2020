using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasKey(e => e.LevelId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.LevelId)
                .HasDatabaseName("LevelId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(40);

            // Define foreign key constraints
            builder.HasOne(d => d.Factor)
                .WithMany(p => p.Level)
                .HasForeignKey(d => d.FactorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LevelFactorId");
        }
    }
}

