using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Region : INamed
    {
        public Region()
        {
            Sites = new HashSet<Site>();
        }

        public int RegionId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<Site> Sites { get; set; }

    }
}
