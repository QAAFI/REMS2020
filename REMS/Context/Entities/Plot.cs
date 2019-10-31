using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Plot : BaseEntity
    {
        public Plot() : base()
        {
            PlotData = new HashSet<PlotData>();
            SoilData = new HashSet<SoilData>();
            SoilLayerData = new HashSet<SoilLayerData>();
        }

        public int PlotId { get; set; }

        public int TreatmentId { get; set; }

        public int? RepetitionNumber { get; set; }

        public int? Columns { get; set; }

        public int? Rows { get; set; }


        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
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
