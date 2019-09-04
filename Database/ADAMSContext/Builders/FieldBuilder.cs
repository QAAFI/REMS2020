using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildField(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FieldId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FieldId)
                    .HasName("FieldId")
                    .IsUnique();

                entity.HasIndex(e => e.SiteId)
                    .HasName("FieldSiteId");

                entity.HasIndex(e => e.SoilId)
                    .HasName("FieldSoilId");

                // Define the table properties
                entity.Property(e => e.FieldId)
                    .HasColumnName("FieldId");

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteId");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(20);

                entity.Property(e => e.Latitude)
                    .HasColumnName("Latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnName("Longitude");

                entity.Property(e => e.Elevation)
                    .HasColumnName("Elevation");

                entity.Property(e => e.Slope)
                    .HasColumnName("Slope")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Depth)
                    .HasColumnName("Depth")
                    .HasDefaultValueSql("0");
                
                // Define foreign key constraints
                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FieldSiteId");

                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FieldSoilId");
            });

        }
    }
}