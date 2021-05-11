
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class ExperimentConfiguration : IEntityTypeConfiguration<Experiment>
    {
        public void Configure(EntityTypeBuilder<Experiment> builder)
        {
            builder.HasKey(e => e.ExperimentId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.ExperimentId)
                .HasDatabaseName("ExperimentId")
                .IsUnique();

            builder.Property(e => e.ExperimentId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasMaxLength(80);

            builder.Property(e => e.Design)
                .HasMaxLength(50);

            builder.Property(e => e.Notes)
                .HasMaxLength(50);

            // Define foreign key constraints
            builder.HasOne(d => d.Crop)
                .WithMany(p => p.Experiments)
                .HasForeignKey(d => d.CropId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ExperimentCropId")
                .IsRequired();

            builder.HasOne(d => d.Field)
                .WithMany(p => p.Experiments)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ExperimentFieldId")
                .IsRequired();

            builder.HasOne(d => d.MetStation)
                .WithMany(p => p.Experiments)
                .HasForeignKey(d => d.MetStationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ExperimentMetStationId")
                .IsRequired();

            builder.HasOne(d => d.Method)
                .WithMany(p => p.Experiments)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ExperimentMethodId");

        }
    }
}
 