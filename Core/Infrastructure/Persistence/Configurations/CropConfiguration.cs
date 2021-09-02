using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class CropConfiguration : IEntityTypeConfiguration<Crop>
    {
        public void Configure(EntityTypeBuilder<Crop> builder)
        {
            builder.HasKey(e => e.CropId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.CropId)
                .HasDatabaseName("CropId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(30);
        }
    }
}


