using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildRegion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.RegionId)
                    .HasName("RegionID");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.Name).HasMaxLength(20);
            });
        }
    }
}