using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Site : IEntity
    {
        public Site()
        {
            Fields = new HashSet<Field>();
        }

        public int SiteId { get; set; }

        public int RegionId { get; set; }

        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Elevation { get; set; }

        public string Notes { get; set; }


        public virtual Region Region { get; set; }
        public virtual ICollection<Field> Fields { get; set; }


    }
}
