using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class SoilLayerData : BaseEntity
    {
        public SoilLayerData() : base()
        { }

        public int SoilLayerDataId { get; set; }

        public int? PlotId { get; set; }

        public int? TraitId { get; set; }

        public DateTime? Date { get; set; }

        public int? DepthFrom { get; set; }

        public int? DepthTo { get; set; }

        public double? Value { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilLayerData>(entity =>
            {

                // Define keys
                entity.HasKey(e => e.SoilLayerDataId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.PlotId)
                    .HasName("SoilLayerDataPlotId");

                entity.HasIndex(e => e.SoilLayerDataId)
                    .HasName("SoilLayerDataId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("SoilLayerDataTraitId");

                // Define properties
                entity.Property(e => e.SoilLayerDataId)
                    .HasColumnName("SoilLayerDataId");

                entity.Property(e => e.DepthFrom)
                    .HasColumnName("DepthFrom")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.DepthTo)
                    .HasColumnName("DepthTo")
                    .HasDefaultValueSql("0");

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
                    .WithMany(p => p.SoilLayerData)
                    .HasForeignKey(d => d.PlotId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilLayerDataPlotId");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilLayerData)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilLayerDataTraitId");
            });

        }
    }
}
