using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasKey(e => e.UnitId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.UnitId)
                .HasDatabaseName("UnitId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(10);
        }
    }
}
