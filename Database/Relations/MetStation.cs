using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("MetStations")]
    public class MetStations
    {
        public MetStations()
        {
            Experiments = new HashSet<Experiment>();
            MetData = new HashSet<MetData>();
            MetInfo = new HashSet<MetInfo>();
        }

        // For use with Activator.CreateInstance
        public MetStations(
            int metStationId,
            string metStationName,
            double? metStationLatitude,
            double? metStationLongitude,
            double? metStationElevation,
            double? amp,
            double? temperatureAverage,
            string notes
        )
        {
            MetStationId = metStationId;
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
    }
}
