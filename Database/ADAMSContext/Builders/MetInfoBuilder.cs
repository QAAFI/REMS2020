using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildMetInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetInfo>(entity =>
            {
                entity.HasIndex(e => e.MetInfoId)
                    .HasName("MetInfoID");

                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetStationsMetInfo");

                entity.HasIndex(e => e.Variable)
                    .HasName("Variable");

                entity.Property(e => e.MetInfoId).HasColumnName("MetInfoID");

                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasMaxLength(20);

                entity.Property(e => e.Variable).HasMaxLength(20);

                entity.HasOne(d => d.MetStation)
                    .WithMany(p => p.MetInfo)
                    .HasForeignKey(d => d.MetStationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MetStationsMetInfo");
            });

        }
    }
}