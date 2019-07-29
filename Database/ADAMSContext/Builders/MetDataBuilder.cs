using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildMetData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetData>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => new {
                        e.MetStationId,
                        e.TraitId,
                        e.Date
                    })
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetDataMetStationId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("MetDataTraitId");

                // Define the table properties
                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationId");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitId");

                entity.Property(e => e.Value)
                    .HasDefaultValueSql("0");

                // Define foregin key constraints
                entity.HasOne(d => d.MetStation)
                    .WithMany(p => p.MetData)
                    .HasForeignKey(d => d.MetStationId)
                    .HasConstraintName("MetDataMetStationId");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.MetData)
                    .HasForeignKey(d => d.TraitId)
                    .HasConstraintName("MetDataTraitId");
            });

        }
    }
}