using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildFactor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factor>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FactorId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FactorId)
                    .HasName("FactorId")
                    .IsUnique();

                // Define the table properties
                entity.Property(e => e.FactorId)
                    .HasColumnName("FactorId");

                entity.Property(e => e.FactorName)
                    .HasMaxLength(20);
            });
        }
    }
}