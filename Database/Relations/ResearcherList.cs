using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("ResearcherList")]
    public class ResearcherList
    {
        [PrimaryKey]
        [Column("ResearcherListId")]
        public int ResearcherListId { get; set; }

        [Column("ResearcherId")]
        public int? ResearcherId { get; set; }

        [Column("ExperimentId")]
        public int? ExperimentId { get; set; }        

        public virtual Experiment Experiment { get; set; }
        public virtual Researcher Researcher { get; set; }
    }
}
