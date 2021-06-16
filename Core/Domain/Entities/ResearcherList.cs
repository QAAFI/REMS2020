using System;
using Rems.Domain.Attributes;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Experiments)]
    public class ResearcherList : IEntity
    {
        public int ResearcherListId { get; set; }

        public int? ResearcherId { get; set; }

        public int ExperimentId { get; set; }

        [Expected("Experiment")]
        public virtual Experiment Experiment { get; set; }

        [Expected("Researcher")]
        public virtual Researcher Researcher { get; set; }
    }
}
