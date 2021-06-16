using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Information)]
    public class Researcher : IEntity
    {
        public Researcher()
        {
            ResearcherLists = new HashSet<ResearcherList>();
        }

        public int ResearcherId { get; set; }

        [Expected("Name", "Researcher")]
        public string Name { get; set; }

        [Expected("Organisation", "Organization", "Org")]
        public string Organisation { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<ResearcherList> ResearcherLists { get; set; }

    }
}
