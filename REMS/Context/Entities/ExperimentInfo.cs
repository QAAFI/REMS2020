using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class ExperimentInfo : BaseEntity
    {
        public ExperimentInfo() : base()
        { }

        public int ExperimentInfoId { get; set; }

        public int ExperimentId { get; set; }

        public string InfoType { get; set; }

        public string Variable { get; set; }

        public string Value { get; set; }


        public virtual Experiment Experiment { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExperimentInfo>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.ExperimentInfoId)
                    .HasName("PrimaryKey");

                // Define the indices
                entity.HasIndex(e => e.ExperimentId)
                    .HasName("ExperimentInfoExperimentId");

                entity.HasIndex(e => e.ExperimentInfoId)
                    .HasName("ExperimentInfoId");

                entity.HasIndex(e => e.Variable)
                    .HasName("ExperimentInfoVariable");

                // Define the properties
                entity.Property(e => e.ExperimentInfoId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ExperimentInfoId");

                entity.Property(e => e.ExperimentId)
                    .HasColumnName("ExperimentID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.InfoType)
                    .HasColumnName("InfoType")
                    .HasMaxLength(1);

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasMaxLength(20);

                entity.Property(e => e.Variable)
                    .HasColumnName("Variable")
                    .HasMaxLength(20);

                // Define the foreign key constraints
                entity.HasOne(d => d.Experiment)
                    .WithMany(p => p.ExperimentInfo)
                    .HasForeignKey(d => d.ExperimentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentInfoExperimentId");
            });
        }
    }
}
