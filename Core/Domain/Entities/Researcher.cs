using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Researcher : IEntity
    {
        public Researcher()
        {
            ResearcherLists = new HashSet<ResearcherList>();
        }

        public int ResearcherId { get; set; }

        public string Name { get; set; }

        public string Organisation { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<ResearcherList> ResearcherLists { get; set; }

    }
}
