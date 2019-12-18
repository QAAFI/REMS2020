using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class CropConfiguration : IEntityTypeConfiguration<Crop>
    {
        public void Configure(EntityTypeBuilder<Crop> builder)
        {
            builder.HasKey(e => e.CropId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.CropId)
                .HasName("CropId")
                .IsUnique();

            builder.Property(e => e.Name).HasMaxLength(30);
        }
    }
}


