using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildFertilizer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fertilizer>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FertilizerId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FertilizerId)
                    .HasName("FertilizerId")
                    .IsUnique();

                // Define the table properties
                entity.Property(e => e.FertilizerId)
                    .HasColumnName("FertilizerId");

                entity.Property(e => e.FertilizerName)
                    .HasMaxLength(60);

                entity.Property(e => e.Calcium)
                    .HasColumnName("Calcium%")
                    .HasDefaultValueSql("0");                

                entity.Property(e => e.Potassium)
                    .HasColumnName("Potassium%")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Nitrogen)
                    .HasColumnName("Nitrogen%")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Phosphorous)
                    .HasColumnName("Phosphorous%")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sulfur)
                    .HasColumnName("Sulfur%")
                    .HasDefaultValueSql("0");
                
                entity.Property(e => e.OtherAmount)
                    .HasColumnName("Other%")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OtherElements)
                    .HasColumnName("OtherElements")
                    .HasMaxLength(20);

                entity.Property(e => e.Notes)
                    .HasMaxLength(100);



            });

        }
    }
}