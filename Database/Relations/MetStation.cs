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

        public int MetStationId { get; set; }
        public string MetStationName { get; set; }
        public double? MetStationLatitude { get; set; }
        public double? MetStationLongitude { get; set; }
        public double? MetStationElevation { get; set; }
        public float? Amp { get; set; }
        public float? TemperatureAverage { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<MetInfo> MetInfo { get; set; }
    }
}
