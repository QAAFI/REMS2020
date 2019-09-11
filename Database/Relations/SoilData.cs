using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
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


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilData>(entity =>
            {
                entity.HasIndex(e => e.PlotId)
                    .HasName("PlotsSoilData");

                entity.HasIndex(e => e.SoilDataId)
                    .HasName("SoilDataID");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitSoilData");

                entity.Property(e => e.SoilDataId).HasColumnName("SoilDataID");

                entity.Property(e => e.PlotId)
                    .HasColumnName("PlotID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasDefaultValueSql("0");

                entity.HasOne(d => d.Plot)
                    .WithMany(p => p.SoilData)
                    .HasForeignKey(d => d.PlotId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlotsSoilData");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilData)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitSoilData");
            });

        }
    }
}
