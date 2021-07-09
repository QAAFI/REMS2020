using System;
using Rems.Domain.Attributes;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, true, "ResearcherList")]
    public class ResearcherList : IEntity
    {
        public int ResearcherListId { get; set; }

        public int? ResearcherId { get; set; }

        public int ExperimentId { get; set; }

        [Expected("Experiment", "ExpID")]
        public virtual Experiment Experiment { get; set; }

        [Expected("Researcher", "ResearcherName", "Researcher Name")]
        public virtual Researcher Researcher { get; set; }
    }
}
