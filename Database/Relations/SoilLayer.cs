using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("SoilLayer")]
    public class SoilLayer
    {
        public SoilLayer()
        {
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
        }

        // For use with Activator.CreateInstance
        public SoilLayer(
            double soilLayerId,
            double soilId,
            int? depthFrom,
            int? depthTo
        )
        {
            SoilLayerId = (int)soilLayerId;
            SoilId = (int)soilId;
            DepthFrom = depthFrom;
            DepthTo = depthTo;
        }

        [PrimaryKey]
        [Column("SoilLayerId")]
        public int SoilLayerId { get; set; }

        [Column("SoilId")]
        public int SoilId { get; set; }

        [Nullable]
        [Column("SoilLayerDepthFrom")]
        public int? DepthFrom { get; set; } = null;

        [Nullable]
        [Column("SoilLayerDepthTo")]
        public int? DepthTo { get; set; } = null;


        public virtual Soil Soil { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilLayer>(entity =>
            {
                entity.HasKey(e => e.SoilLayerId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilsSoilDepth");

                entity.HasIndex(e => e.SoilLayerId)
                    .HasName("SoilDepthID");

                entity.Property(e => e.SoilLayerId).HasColumnName("SoilLayerID");

                entity.Property(e => e.DepthFrom).HasDefaultValueSql("0");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.DepthTo).HasDefaultValueSql("0");

                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.SoilLayers)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilsSoilDepth");
            });

        }
    }
}
