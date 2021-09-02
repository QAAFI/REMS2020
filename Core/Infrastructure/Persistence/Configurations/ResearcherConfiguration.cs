using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class ResearcherConfiguration : IEntityTypeConfiguration<Researcher>
    {
        public void Configure(EntityTypeBuilder<Researcher> builder)
        {
            builder.HasKey(e => e.ResearcherId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.ResearcherId)
                .HasDatabaseName("ResearcherId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.Property(e => e.Organisation)
                .HasMaxLength(15);
        }
    }
}
