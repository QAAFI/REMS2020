using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Researcher
    {
        public Researcher()
        {
            ResearcherLists = new HashSet<ResearcherList>();
        }

        public int ResearcherId { get; set; }
        public string ResearcherName { get; set; }
        public string Organisation { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<ResearcherList> ResearcherLists { get; set; }
    }
}
