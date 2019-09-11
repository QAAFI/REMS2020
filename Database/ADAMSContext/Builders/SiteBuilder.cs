using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildSite(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(e => e.SiteId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.RegionId)
                    .HasName("RegionsSites");

                entity.HasIndex(e => e.SiteId)
                    .HasName("SiteID123");

                entity.HasIndex(e => e.Name)
                    .HasName("SiteName");

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasMaxLength(50);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("RegionsSites");
            });

        }
    }
}