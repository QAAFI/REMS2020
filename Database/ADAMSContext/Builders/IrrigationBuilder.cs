using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildIrrigation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Irrigation>(entity =>
            {
                entity.HasKey(e => e.IrrigationId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.IrrigationId)
                    .HasName("IrrigID");

                entity.HasIndex(e => e.IrrigationMethodId)
                    .HasName("MethodIrrigation");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsIrrigation");

                entity.Property(e => e.IrrigationId).HasColumnName("IrrigID");

                entity.Property(e => e.Amount).HasDefaultValueSql("0");

                entity.Property(e => e.IrrigationMethodId)
                    .HasColumnName("IrrigMethodID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Notes).HasMaxLength(50);

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.IrrigationMethod)
                    .WithMany(p => p.Irrigations)
                    .HasForeignKey(d => d.IrrigationMethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MethodIrrigation");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Irrigations)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsIrrigation");
            });

        }
    }
}