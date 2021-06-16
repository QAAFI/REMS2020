using Rems.Domain.Attributes;
using System;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Experiments)]
    public class Sowing : IEntity
    {
        public int SowingId { get; set; }

        public int ExperimentId { get; set; }

        public int MethodId { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        [Expected("Cultivar")]
        public string Cultivar { get; set; }

        [Expected("Depth")]
        public double Depth { get; set; }

        [Expected("RowSpace", "RowSpan")]
        public double RowSpace { get; set; }

        [Expected("Population")]
        public double Population { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        [Expected("Experiment")]
        public virtual Experiment Experiment { get; set; }

        [Expected("Method")]
        public virtual Method Method { get; set; }
    }
}
