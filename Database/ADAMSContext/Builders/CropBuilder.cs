using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildCrop(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crop>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.CropId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.CropId)
                    .HasName("CropId")
                    .IsUnique();              

                // Define the table properties
                entity.Property(e => e.CropId)
                    .HasColumnName("CropId");

                entity.Property(e => e.CropName)
                    .HasColumnName("Name")
                    .HasMaxLength(30);
            });
        }
    }
}