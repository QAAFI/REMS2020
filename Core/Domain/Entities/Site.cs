using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource("Information", "Sites")]
    public class Site : IEntity
    {
        public Site()
        {
            Fields = new HashSet<Field>();
        }

        public int SiteId { get; set; }

        public int RegionId { get; set; }

        [Expected("Name", "Site Name", "SiteName", "Site")]
        public string Name { get; set; }

        [Expected("Latitude", "Lat")]
        public double? Latitude { get; set; } = null;

        [Expected("Longitude", "Lon")]
        public double? Longitude { get; set; } = null;

        [Expected("Elevation")]
        public double? Elevation { get; set; } = null;

        [Expected("Notes")]
        public string Notes { get; set; }

        [Expected("Region", "RegionName", "Region Name")]
        public virtual Region Region { get; set; }

        public virtual ICollection<Field> Fields { get; set; }


    }
}
