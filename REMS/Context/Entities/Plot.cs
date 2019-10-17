using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    [Relation("Plot")]
    public class Plot
    {
        public Plot()
        {
            PlotData = new HashSet<PlotData>();
            SoilData = new HashSet<SoilData>();
            SoilLayerData = new HashSet<SoilLayerData>();
        }

        [PrimaryKey]
        [Column("PlotId")]
        public int PlotId { get; set; }

        [Column("TreatmentId")]
        public int TreatmentId { get; set; }

        [Nullable]
        [Column("RepetitionNumber")]
        public int? RepetitionNumber { get; set; }

        [Nullable]
        [Column("Columns")]
        public int? Columns { get; set; }

        [Nullable]
        [Column("Rows")]
        public int? Rows { get; set; }


        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plot>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.PlotId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.PlotId)
                    .HasName("PlotId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("PlotTreatmentId");

                // Define properties
                entity.Property(e => e.PlotId)
                    .HasColumnName("PlotId");

                entity.Property(e => e.Columns)
                    .HasColumnName("Columns")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RepetitionNumber)
                    .HasColumnName("RepetitionNumber")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Rows)
                    .HasColumnName("Rows")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId")
                    .HasDefaultValueSql("0");

                // Define constraints
                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Plots)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsPlots");
            });

        }
    }
}
