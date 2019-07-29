using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildIrrigation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Irrigation>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.IrrigationId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.IrrigationId)
                    .HasName("IrrigationId")
                    .IsUnique();

                entity.HasIndex(e => e.IrrigationMethodId)
                    .HasName("IrrigationMethodId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("IrrigationTreatmentId");

                // Define the table properties
                entity.Property(e => e.IrrigationId)
                    .HasColumnName("IrrigationId");

                entity.Property(e => e.Amount)
                    .HasColumnName("Amount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IrrigationMethodId)
                    .HasColumnName("IrrigationMethodID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(50);

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId");

                // Define foreign key constraints
                entity.HasOne(d => d.IrrigationMethod)
                    .WithMany(p => p.Irrigations)
                    .HasForeignKey(d => d.IrrigationMethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("IrrigationMethodId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Irrigations)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("IrrigationTreatmentId");
            });

        }
    }
}