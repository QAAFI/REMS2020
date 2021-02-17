using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class MetStation : IEntity
    {
        public MetStation()
        {
            Experiments = new HashSet<Experiment>();
            MetData = new HashSet<MetData>();
            MetInfo = new HashSet<MetInfo>();
        }

        public int MetStationId { get; set; }

        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Elevation { get; set; }

        public double? Amplitude { get; set; }

        public double? TemperatureAverage { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<MetInfo> MetInfo { get; set; }

    }
}
