using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Region")]
    public class Region
    {
        public Region()
        {
            Sites = new HashSet<Site>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
    }
}
