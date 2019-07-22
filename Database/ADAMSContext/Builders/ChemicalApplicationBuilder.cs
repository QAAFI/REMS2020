using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildChemicalApplication(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChemicalApplication>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.ApplicationId)                
                    .HasName("PrimaryKey");                

                // Define the table indices
                entity.HasIndex(e => e.ApplicationId)
                    .HasName("ApplicationID")
                    .IsUnique();

                entity.HasIndex(e => e.MethodId)
                    .HasName("ApplicationMethodId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("ApplicationTreatmentId");

                entity.HasIndex(e => e.UnitId)
                    .HasName("ApplicationUnitId");

                // Define the table properties
                entity.Property(e => e.ApplicationId)
                    .HasColumnName("ApplicationId");

                entity.Property(e => e.Amount)
                    .HasColumnName("Amount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodId");

                entity.Property(e => e.Notes)
                    .HasColumnName("Notes")
                    .HasMaxLength(50);

                entity.Property(e => e.Target)
                    .HasColumnName("Target")
                    .HasMaxLength(25);

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitId");

                // Define foreign key constraints
                entity.HasOne(d => d.ApplicationMethod)
                    .WithMany(p => p.ChemicalApplications)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ApplicationMethodId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.ChemicalApplications)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ApplicationTreatmentId");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ChemicalApplications)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ApplicationUnitId");
            });
        }
    }
}