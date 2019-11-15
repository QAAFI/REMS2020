using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class MetDataConfiguration : IEntityTypeConfiguration<MetData>
    {
        public void Configure(EntityTypeBuilder<MetData> builder)
        {
            builder.HasKey(e => new {
                e.MetStationId,
                e.TraitId,
                e.Date
            })
            .HasName("PrimaryKey");

            // Define the table indices
            builder.HasIndex(e => e.MetStationId)
                .HasName("MetDataMetStationId");

            builder.HasIndex(e => e.TraitId)
                .HasName("MetDataTraitId");

            builder.HasIndex(e => e.Date)
                .HasName("MetDataDate");

            // Define the table properties

            builder.Property(e => e.Value)
                .HasColumnName("Value")
                .HasDefaultValueSql("0");

            // Define foregin key constraints
            builder.HasOne(d => d.MetStation)
                .WithMany(p => p.MetData)
                .HasForeignKey(d => d.MetStationId)
                .HasConstraintName("MetDataMetStationId");

            builder.HasOne(d => d.Trait)
                .WithMany(p => p.MetData)
                .HasForeignKey(d => d.TraitId)
                .HasConstraintName("MetDataTraitId");
        }
    }
}
