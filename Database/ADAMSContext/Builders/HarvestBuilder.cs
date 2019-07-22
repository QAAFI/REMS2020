using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildHarvest(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Harvest>(entity =>
            {
                entity.HasIndex(e => e.HarvestId)
                    .HasName("HarvestID");

                entity.HasIndex(e => e.HarvestMethodId)
                    .HasName("MethodHarvest");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsHarvest");

                entity.Property(e => e.HarvestId).HasColumnName("HarvestID");

                entity.Property(e => e.HarvestMethodId)
                    .HasColumnName("HarvestMethodID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Notes).HasMaxLength(50);

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.HarvestMethod)
                    .WithMany(p => p.Harvests)
                    .HasForeignKey(d => d.HarvestMethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MethodHarvest");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Harvests)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsHarvest");
            });

        }
    }
}