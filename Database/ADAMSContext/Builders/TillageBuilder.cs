using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildTillage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tillage>(entity =>
            {
                entity.HasIndex(e => e.TillageId)
                    .HasName("TillageID");

                entity.HasIndex(e => e.TillageMethodId)
                    .HasName("TILLAGE IMPLEMENT_CODE");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsTillage");

                entity.Property(e => e.TillageId).HasColumnName("TillageID");

                entity.Property(e => e.TillageNotes).HasMaxLength(50);

                entity.Property(e => e.TillageMethodId).HasColumnName("TillageMethodID");

                entity.Property(e => e.TreatmentId).HasColumnName("TreatmentID");

                entity.HasOne(d => d.TillageMethod)
                    .WithMany(p => p.Tillages)
                    .HasForeignKey(d => d.TillageMethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MethodTillage");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Tillages)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsTillage");
            });

        }
    }
}