using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("ExperimentInfo")]
    public class ExperimentInfo
    {
        // For use with Activator.CreateInstance
        public ExperimentInfo(
            int experimentInfoId,
            int experimentId,
            string infoType,
            string variable,
            string value
        )
        {
            ExperimentInfoId = experimentInfoId;
            ExperimentId = experimentId;
            InfoType = infoType;
            Variable = variable;
            Value = value;
        }

        [PrimaryKey]
        [Column("ExperimentInfoId")]
        public int ExperimentInfoId { get; set; }

        [Column("ExperimentId")]
        public int ExperimentId { get; set; }

        [Nullable]
        [Column("InfoType")]
        public string InfoType { get; set; }

        [Nullable]
        [Column("Variable")]
        public string Variable { get; set; }

        [Nullable]
        [Column("Value")]
        public string Value { get; set; }


        public virtual Experiment Experiment { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExperimentInfo>(entity =>
            {
                entity.HasKey(e => e.ExperimentInfoId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.ExperimentId)
                    .HasName("TestNameID");

                entity.HasIndex(e => e.ExperimentInfoId)
                    .HasName("OtherTestNameID");

                entity.HasIndex(e => e.Variable)
                    .HasName("LevelID149");

                entity.Property(e => e.ExperimentInfoId).HasColumnName("ExpInfoID");

                entity.Property(e => e.ExperimentId)
                    .HasColumnName("ExpID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.InfoType).HasMaxLength(1);

                entity.Property(e => e.Value).HasMaxLength(20);

                entity.Property(e => e.Variable).HasMaxLength(20);

                entity.HasOne(d => d.Experiment)
                    .WithMany(p => p.ExperimentInfo)
                    .HasForeignKey(d => d.ExperimentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentsExpInfo");
            });
        }
    }
}
