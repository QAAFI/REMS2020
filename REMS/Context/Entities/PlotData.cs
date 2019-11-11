using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class PlotData : BaseEntity
    {
        public PlotData() : base()
        { }

        public int PlotDataId { get; set; }

        public int? PlotId { get; set; }

        public int? TraitId { get; set; }

        public DateTime? PlotDataDate { get; set; }

        public string Sample { get; set; }

        public double? Value { get; set; }

        public int? UnitId { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
        public virtual Unit Unit { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlotData>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.PlotDataId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.PlotDataId)
                    .HasName("PlotDataId");

                entity.HasIndex(e => e.PlotId)
                    .HasName("PlotDataPlotId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("PlotDataTraitId");

                entity.HasIndex(e => e.UnitId)
                    .HasName("PlotDataUnitId");

                // Define properties
                entity.Property(e => e.PlotDataId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PlotDataID");

                entity.Property(e => e.PlotId)
                    .HasColumnName("PlotID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sample)
                    .HasColumnName("Sample")
                    .HasMaxLength(10);

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasDefaultValueSql("0");

                // Define constraints
                entity.HasOne(d => d.Plot)
                    .WithMany(p => p.PlotData)
                    .HasForeignKey(d => d.PlotId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlotDataPlotId");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.PlotData)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlotDataTraitId");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.PlotData)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlotDataUnitId");
            });

        }
    }
}
