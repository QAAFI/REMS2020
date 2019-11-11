using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class SoilLayer : BaseEntity
    {
        public SoilLayer() : base()
        {
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
        }

        public int SoilLayerId { get; set; }

        public int SoilId { get; set; }

        public int? DepthFrom { get; set; } = null;

        public int? DepthTo { get; set; } = null;


        public virtual Soil Soil { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilLayer>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.SoilLayerId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilLayerSoilId");

                entity.HasIndex(e => e.SoilLayerId)
                    .HasName("SoilLayerId");

                // Define properties
                entity.Property(e => e.SoilLayerId)
                    .HasColumnName("SoilLayerId");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.DepthFrom)
                    .HasColumnName("DepthFrom")
                    .HasDefaultValueSql("0");                

                entity.Property(e => e.DepthTo)
                    .HasColumnName("DepthTo")
                    .HasDefaultValueSql("0");

                // Define constraints
                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.SoilLayers)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilsSoilDepth");
            });

        }
    }
}
