using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildSoil(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Soil>(entity =>
            {
                entity.HasKey(e => e.SoilId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilID586");

                entity.Property(e => e.SoilId).HasColumnName("SoilID");

                entity.Property(e => e.SoilType).HasMaxLength(30);
            });
        }
    }
}