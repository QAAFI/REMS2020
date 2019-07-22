using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildSoilLayer(ModelBuilder modelBuilder)
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

                entity.Property(e => e.SoilLayerDepthFrom).HasDefaultValueSql("0");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SoilLayerDepthTo).HasDefaultValueSql("0");

                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.SoilLayers)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilsSoilDepth");
            });

        }
    }
}