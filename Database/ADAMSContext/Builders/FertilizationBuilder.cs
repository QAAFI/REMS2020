using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildFertilization(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fertilization>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FertilizationId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FertilizationId)
                    .HasName("FertilizationId")
                    .IsUnique();

                entity.HasIndex(e => e.FertilizerId)
                    .HasName("FertilizationFertilizerId");

                entity.HasIndex(e => e.MethodId)
                    .HasName("FertilizationMethodId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("FertilizationTreatmentId");

                entity.HasIndex(e => e.UnitId)
                    .HasName("FertilizationUnitId");

                // Define the table properties
                entity.Property(e => e.FertilizationId)
                    .HasColumnName("FertilizationId");

                entity.Property(e => e.FertilizationAmount)
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FertilizationDepth)
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FertilizerId)
                    .HasColumnName("FertilizerId");

                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(100);

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitId")
                    .HasDefaultValueSql("0");

                // Define foreign key constraints
                entity.HasOne(d => d.Fertilizer)
                    .WithMany(p => p.Fertilization)
                    .HasForeignKey(d => d.FertilizerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationFertilizerId");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.Fertilizations)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationMethodId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Fertilizations)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationTreatmentId");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Fertilizations)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationUnitId");
            });
        }
    }
}