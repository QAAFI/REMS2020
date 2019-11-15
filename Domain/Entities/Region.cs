using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Region
    {
        public Region()
        {
            Sites = new HashSet<Site>();
        }

        public int RegionId { get; set; }

        public string Name { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual ICollection<Site> Sites { get; set; }

    }
}
