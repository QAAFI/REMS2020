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

        public int SiteId { get; set; }
        public int? RegionId { get; set; }
        public string SiteName { get; set; }
        public double? SiteLatitude { get; set; }
        public double? SiteLongitude { get; set; }
        public double? SiteElevation { get; set; }
        public string SiteNotes { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
    }
}
