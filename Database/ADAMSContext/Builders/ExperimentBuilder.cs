using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildExperiment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Experiment>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.ExperimentId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.ExperimentId)
                    .HasName("ExperimentId")
                    .IsUnique();

                entity.HasIndex(e => e.ExperimentName)
                    .HasName("ExperimentName");

                entity.HasIndex(e => e.CropId)
                    .HasName("ExperimentCropId");                                
                
                entity.HasIndex(e => e.FieldId)
                    .HasName("ExperimentFieldId");

                entity.HasIndex(e => e.MetStationId)
                    .HasName("ExperimentMetStationId");

                entity.HasIndex(e => e.MethodId)
                    .HasName("ExperimentMethodId");

                // Define the table properties
                entity.Property(e => e.ExperimentId)
                    .HasColumnName("ExperimentId");

                entity.Property(e => e.CropId)
                    .HasColumnName("CropId");

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(80);

                entity.Property(e => e.ExperimentDesign)
                    .HasColumnName("ExperimentDesign")
                    .HasMaxLength(50);

                entity.Property(e => e.ExperimentName)
                    .HasMaxLength(50);

                entity.Property(e => e.FieldId)
                    .HasColumnName("FieldId");

                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationId");

                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodId");

                entity.Property(e => e.Rating);

                entity.Property(e => e.Repetitions);

                // Define foreign key constraints
                entity.HasOne(d => d.Crop)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.CropId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentCropId");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentFieldId");

                entity.HasOne(d => d.MetStation)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.MetStationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentMetStationId");

                entity.HasOne(d => d.PlantingMethod)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentMethodId");
            });
        }
    }
}