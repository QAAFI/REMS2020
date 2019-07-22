using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildMetData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetData>(entity =>
            {
                entity.HasKey(e => new {
                        e.MetStationId,
                        e.TraitId,
                        e.Date
                    })
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetStationsMetData");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitMetData");

                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.MetStation)
                    .WithMany(p => p.MetData)
                    .HasForeignKey(d => d.MetStationId)
                    .HasConstraintName("MetStationsMetData");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.MetData)
                    .HasForeignKey(d => d.TraitId)
                    .HasConstraintName("TraitMetData");
            });

        }
    }
}