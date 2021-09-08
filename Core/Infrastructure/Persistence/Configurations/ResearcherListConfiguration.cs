using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Infrastructure.Persistence.Configurations
{
    public class ResearcherListConfiguration : IEntityTypeConfiguration<ResearcherList>
    {
        public void Configure(EntityTypeBuilder<ResearcherList> builder)
        {
            builder.HasKey(e => e.ResearcherListId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.ResearcherListId)
                .HasDatabaseName("ResearcherListId")
                .IsUnique();

            builder.Property(e => e.ExperimentId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.ResearcherId)
                .HasDefaultValueSql("0");

            // Define constraints
            builder.HasOne(d => d.Experiment)
                .WithMany(p => p.ResearcherList)
                .HasForeignKey(d => d.ExperimentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ResearcherListExperimentId");

            builder.HasOne(d => d.Researcher)
                .WithMany(p => p.ResearcherLists)
                .HasForeignKey(d => d.ResearcherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ResearcherListResearcherId");
        }
    }
}
