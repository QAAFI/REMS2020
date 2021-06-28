using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Information", 0, true, "MetStations")]
    public class MetStation : IEntity
    {
        public MetStation()
        {
            Experiments = new HashSet<Experiment>();
            MetData = new HashSet<MetData>();
            MetInfo = new HashSet<MetInfo>();
        }

        public int MetStationId { get; set; }

        [Expected("Name", "MetStation", "MetStation Name", "Station")]
        public string Name { get; set; }

        [Expected("Latitude", "Lat")]
        public double? Latitude { get; set; } = null;

        [Expected("Longitude", "Lon")]
        public double? Longitude { get; set; } = null;

        [Expected("Elevation")]
        public double? Elevation { get; set; } = null;

        [Expected("Amplitude", "amp")]
        public double? Amplitude { get; set; } = null;

        [Expected("TemperatureAverage", "tav")]
        public double? TemperatureAverage { get; set; } = null;

        [Expected("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<MetInfo> MetInfo { get; set; }

    }
}
