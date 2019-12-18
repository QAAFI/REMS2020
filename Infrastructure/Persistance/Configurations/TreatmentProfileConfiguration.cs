using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Persistence.Configurations
{
    public class TreatmentProfileConfiguration : IEntityTypeConfiguration<TreatmentProfile>
    {
        public void Configure(EntityTypeBuilder<TreatmentProfile> builder)
        {
            builder.HasKey(e => e.TreatmentProfileId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.TreatmentProfileId)
                .HasName("TreatmentProfileId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.HasOne(d => d.Experiment)
                .WithMany(p => p.TreatmentProfiles)
                .HasForeignKey(d => d.ExperimentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TreatmentProfileExperimentId");
        }
    }
}
