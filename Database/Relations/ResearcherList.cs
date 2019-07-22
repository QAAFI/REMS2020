using System;
using System.Collections.Generic;

namespace Database
{
    public partial class ResearcherList
    {
        public int ResearcherListId { get; set; }
        public int? ExperimentId { get; set; }
        public int? ResearcherId { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual Researcher Researcher { get; set; }
    }
}
