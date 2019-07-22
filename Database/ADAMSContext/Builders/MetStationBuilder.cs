using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildMetStation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetStations>(entity =>
            {
                entity.HasKey(e => e.MetStationId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetStationID");

                entity.Property(e => e.MetStationId).HasColumnName("MetStationID");

                entity.Property(e => e.Amp)
                    .HasColumnName("amp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MetStationName).HasMaxLength(40);

                entity.Property(e => e.TemperatureAverage)
                    .HasColumnName("tav")
                    .HasDefaultValueSql("0");
            });

        }
    }
}