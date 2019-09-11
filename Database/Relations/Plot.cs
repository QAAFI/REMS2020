using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
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
        [Column("Repetitions")]
        public int? Repetitions { get; set; }

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


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plot>(entity =>
            {
                entity.HasKey(e => e.PlotId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.PlotId)
                    .HasName("PlotID");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsPlots");

                entity.Property(e => e.PlotId).HasColumnName("PlotID");

                entity.Property(e => e.Columns).HasDefaultValueSql("0");

                entity.Property(e => e.Repetitions).HasDefaultValueSql("0");

                entity.Property(e => e.Rows).HasDefaultValueSql("0");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Plots)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsPlots");
            });

        }
    }
}
