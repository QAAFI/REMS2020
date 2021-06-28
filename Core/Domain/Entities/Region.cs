using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Information", 0, true, "Regions")]
    public class Region : IEntity
    {
        public Region()
        {
            Sites = new HashSet<Site>();
        }

        public int RegionId { get; set; }

        [Expected("Name", "Region", "Region Name")]
        public string Name { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }


        public virtual ICollection<Site> Sites { get; set; }

    }
}
