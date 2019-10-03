using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("MetStations")]
    public class MetStation
    {
        public MetStation()
        {
            Experiments = new HashSet<Experiment>();
            MetData = new HashSet<MetData>();
            MetInfo = new HashSet<MetInfo>();
        }

        // For use with Activator.CreateInstance
        public MetStation(
            double metStationId,
            string metStationName,
            double? metStationLatitude,
            double? metStationLongitude,
            double? metStationElevation,
            double? amp,
            double? temperatureAverage,
            string notes
        )
        {
            MetStationId = (int)metStationId;
            Name = metStationName;
            Latitude = metStationLatitude;
            Longitude = metStationLongitude;
            Elevation = metStationElevation;
            Amp = amp;
            TemperatureAverage = temperatureAverage;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("MetStationId")]
        public int MetStationId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; }

        [Nullable]
        [Column("Latitude")]
        public double? Latitude { get; set; }

        [Nullable]
        [Column("Longitude")]
        public double? Longitude { get; set; }

        [Nullable]
        [Column("Elevation")]
        public double? Elevation { get; set; }

        [Nullable]
        [Column("Amp")]
        public double? Amp { get; set; }

        [Nullable]
        [Column("TemperatureAverage")]
        public double? TemperatureAverage { get; set; }

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<MetInfo> MetInfo { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetStation>(entity =>
            {
                // Define the keys
                entity.HasKey(e => e.MetStationId)
                    .HasName("PrimaryKey");

                // Define the indices
                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetStationId");

                // Define the properties
                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationId");

                entity.Property(e => e.Amp)
                    .HasColumnName("Amp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(40);

                entity.Property(e => e.TemperatureAverage)
                    .HasColumnName("TemperatureAverage")
                    .HasDefaultValueSql("0");
            });

        }
    }
}
