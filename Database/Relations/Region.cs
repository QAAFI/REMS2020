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

        // For use with Activator.CreateInstance
        public Region(
            int regionId,
            string name,
            string notes
        )
        {
            RegionId = regionId;
            Name = name;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("RegionId")]
        public int RegionId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual ICollection<Site> Sites { get; set; }
    }
}
