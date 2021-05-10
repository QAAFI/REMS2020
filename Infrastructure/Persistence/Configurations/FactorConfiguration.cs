using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class FactorConfiguration : IEntityTypeConfiguration<Factor>
    {
        public void Configure(EntityTypeBuilder<Factor> builder)
        {
            builder.HasKey(e => e.FactorId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.FactorId)
                .HasDatabaseName("FactorId")
                .IsUnique();

            builder.Property(e => e.Name).HasMaxLength(20);
        }
    }
}


