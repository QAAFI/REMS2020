using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Experiment  : BaseEntity
    {
        public Experiment() : base()
        {
            ExperimentInfo = new HashSet<ExperimentInfo>();
            ResearcherList = new HashSet<ResearcherList>();
            Treatments = new HashSet<Treatment>();
        }

        public int ExperimentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? CropId { get; set; }

        public int? FieldId { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? MetStationId { get; set; }

        public string Design { get; set; }

        public short? Repetitions { get; set; }

        public int? Rating { get; set; }

        public string Notes { get; set; }

        public int? MethodId { get; set; }

        public string PlantingNotes { get; set; }


        public virtual Crop Crop { get; set; }
        public virtual Field Field { get; set; }
        public virtual MetStation MetStation { get; set; }
        public virtual Method PlantingMethod { get; set; }

        public virtual ICollection<ExperimentInfo> ExperimentInfo { get; set; }
        public virtual ICollection<ResearcherList> ResearcherList { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }

        public override void BuildModel(ModelBuilder modelBuilder)
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

                entity.HasIndex(e => e.Name)
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

                entity.Property(e => e.Design)
                    .HasColumnName("Design")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
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
