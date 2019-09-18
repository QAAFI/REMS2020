using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("SoilData")]
    public class SoilData
    {
        public SoilData()
        { }

        // For use with Activator.CreateInstance
        public SoilData(
            int soilDataId,
            int plotId,
            int traitId,
            DateTime soilDataDate,
            double value
        )
        {
            SoilDataId = soilDataId;
            PlotId = plotId;
            TraitId = traitId;
            SoilDataDate = soilDataDate;
            Value = value;
        }

        [PrimaryKey]
        [Column("SoilDataId")]
        public int SoilDataId { get; set; }

        [Column("PlotId")]
        public int PlotId { get; set; }

        [Column("TraitId")]
        public int TraitId { get; set; }

        [Nullable]
        [Column("Date")]
        public DateTime? SoilDataDate { get; set; }

        [Nullable]
        [Column("Value")]
        public double? Value { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilData>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.SoilDataId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.PlotId)
                    .HasName("SoilDataPlotId");

                entity.HasIndex(e => e.SoilDataId)
                    .HasName("SoilDataId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("SoilDataTraitId");

                // Define properties
                entity.Property(e => e.SoilDataId)
                    .HasColumnName("SoilDataId");

                entity.Property(e => e.PlotId)
                    .HasColumnName("PlotId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasDefaultValueSql("0");

                // Define constraints
                entity.HasOne(d => d.Plot)
                    .WithMany(p => p.SoilData)
                    .HasForeignKey(d => d.PlotId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilDataPlotId");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilData)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilDataTraitId");
            });

        }
    }
}
