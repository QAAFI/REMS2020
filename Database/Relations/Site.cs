using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Site")]
    public class Site
    {
        public Site()
        {
            Fields = new HashSet<Field>();
        }

        // For use with Activator.CreateInstance
        public Site(
            double siteId,
            double regionId,
            string name,
            double? latitude,
            double? longitude,
            double? elevation,
            string notes
        )
        {
            SiteId = (int)siteId;
            RegionId = (int)regionId;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("SiteId")]
        public int SiteId { get; set; }

        [Column("RegionId")]
        public int RegionId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; } = null;

        [Nullable]
        [Column("Latitude")]
        public double? Latitude { get; set; } = null;

        [Nullable]
        [Column("Longitude")]
        public double? Longitude { get; set; } = null;

        [Nullable]
        [Column("Elevation")]
        public double? Elevation { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual Region Region { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
    }
}
