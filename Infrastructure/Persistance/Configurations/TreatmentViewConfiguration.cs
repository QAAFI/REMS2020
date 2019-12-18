using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Persistence.Configurations
{
    public class TreatmentViewConfiguration : IEntityTypeConfiguration<TreatmentView>
    {
        public void Configure(EntityTypeBuilder<TreatmentView> builder)
        {
            builder.HasKey(e => e.TreatmentViewId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.TreatmentViewId)
                .HasName("TreatmentViewId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.HasOne(d => d.Experiment)
                .WithMany(p => p.TreatmentViews)
                .HasForeignKey(d => d.ExperimentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TreatmentViewExperimentId");

            builder.HasOne(d => d.TreatmentProfile)
                .WithOne(p => p.TreatmentView)
                .HasForeignKey<TreatmentProfile>(d => d.TreatmentProfileId);
        }
    }
}
