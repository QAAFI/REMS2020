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

        public string Name { get; set; } = null;

        public double? Latitude { get; set; } = null;

        public double? Longitude { get; set; } = null;

        public double? Elevation { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual Region Region { get; set; }
        public virtual ICollection<Field> Fields { get; set; }


    }
}
