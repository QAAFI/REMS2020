using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class MethodConfiguration : IEntityTypeConfiguration<Method>
    {
        public void Configure(EntityTypeBuilder<Method> builder)
        {
            builder.HasKey(e => e.MethodId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.MethodId)
                .HasDatabaseName("MethodId")
                .IsUnique();

            builder.Property(e => e.Name).HasMaxLength(30);

            builder.Property(e => e.Type).HasMaxLength(15);
        }
    }
}
