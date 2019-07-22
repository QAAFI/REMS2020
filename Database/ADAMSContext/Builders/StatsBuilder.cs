using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildStats(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stats>(entity =>
            {
                entity.HasIndex(e => e.StatsId)
                    .HasName("StatsID");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitStats");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsStats");

                entity.HasIndex(e => e.UnitId)
                    .HasName("UnitsStats");

                entity.Property(e => e.StatsId).HasColumnName("StatsID");

                entity.Property(e => e.Mean).HasDefaultValueSql("0");

                entity.Property(e => e.N)
                    .HasColumnName("n")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Se)
                    .HasColumnName("SE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitStats");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsStats");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UnitsStats");
            });

        }
    }
}