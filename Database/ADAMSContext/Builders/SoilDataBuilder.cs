using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildSoilData(ModelBuilder modelBuilder)
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