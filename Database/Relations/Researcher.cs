using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Researcher")]
    public class Researcher
    {
        public Researcher()
        {
            ResearcherLists = new HashSet<ResearcherList>();
        }

        // For use with Activator.CreateInstance
        public Researcher(
            int researcherId,
            string name,
            string organisation,
            string notes
        )
        {
            ResearcherId = researcherId;
            Name = name;
            Organisation = organisation;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("ResearcherId")]
        public int ResearcherId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; }

        [Nullable]
        [Column("Organisation")]
        public string Organisation { get; set; }

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<ResearcherList> ResearcherLists { get; set; }
    }
}
