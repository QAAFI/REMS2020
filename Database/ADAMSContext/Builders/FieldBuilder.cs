using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildField(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>(entity =>
            {
                entity.HasKey(e => e.FieldId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.FieldId)
                    .HasName("FieldID");

                entity.HasIndex(e => e.SiteId)
                    .HasName("SitesFields");

                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.Depth).HasDefaultValueSql("0");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Slope).HasDefaultValueSql("0");

                entity.Property(e => e.SoilId).HasColumnName("SoilID");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SitesFields");

                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilField");
            });

        }
    }
}