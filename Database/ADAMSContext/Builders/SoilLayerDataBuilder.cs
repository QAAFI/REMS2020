using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildSoilLayerData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilLayerData>(entity =>
            {
                entity.HasKey(e => e.SoilLayerId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.PlotId)
                    .HasName("PlotsSoilLayerData");

                entity.HasIndex(e => e.SoilLayerId)
                    .HasName("SoilLayerID");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitsSoilLayerData");

                entity.Property(e => e.SoilLayerId).HasColumnName("SoilLayerID");

                entity.Property(e => e.SoilLayerDataDepthFrom).HasDefaultValueSql("0");

                entity.Property(e => e.SoilLayerDataDepthTo).HasDefaultValueSql("0");

                entity.Property(e => e.PlotId)
                    .HasColumnName("PlotID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasDefaultValueSql("0");

                entity.HasOne(d => d.Plot)
                    .WithMany(p => p.SoilLayerData)
                    .HasForeignKey(d => d.PlotId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlotsSoilLayerData");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilLayerData)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitsSoilLayerData");
            });

        }
    }
}