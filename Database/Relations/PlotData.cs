using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("PlotData")]
    public class PlotData
    {
        [PrimaryKey]
        [Column("PlotDataId")]
        public int PlotDataId { get; set; }

        [Column("PlotId")]
        public int? PlotId { get; set; }

        [Column("TraitId")]
        public int? TraitId { get; set; }

        [Column("PlotDataDate")]
        public DateTime? PlotDataDate { get; set; }

        [Column("Sample")]
        public string Sample { get; set; }

        [Column("Value")]
        public double? Value { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
        public virtual Unit Unit { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlotData>(entity =>
            {
                entity.HasIndex(e => e.PlotDataId)
                    .HasName("PlotDataID");

                entity.HasIndex(e => e.PlotId)
                    .HasName("PlotsPlotData");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitPlotData");

                entity.HasIndex(e => e.UnitId)
                    .HasName("UnitsPlotData");

                entity.Property(e => e.PlotDataId).HasColumnName("PlotDataID");

                entity.Property(e => e.PlotId)
                    .HasColumnName("PlotID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sample).HasMaxLength(10);

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasDefaultValueSql("0");

                entity.HasOne(d => d.Plot)
                    .WithMany(p => p.PlotData)
                    .HasForeignKey(d => d.PlotId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlotsPlotData");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.PlotData)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitPlotData");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.PlotData)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UnitsPlotData");
            });

        }
    }
}
