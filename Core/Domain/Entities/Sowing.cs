using Rems.Domain.Attributes;
using System;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, true, "Sowing", "Planting")]
    public class Sowing : IEntity
    {
        public int SowingId { get; set; }

        public int ExperimentId { get; set; }

        public int MethodId { get; set; }

        [Expected("Date", "PlantingDate")]
        public DateTime Date { get; set; }

        [Expected("Cultivar")]
        public string Cultivar { get; set; }

        [Expected("Depth", "PlantingDepth", "Planting Depth")]
        public double Depth { get; set; }

        [Expected("RowSpace", "RowSpan", "Row Space")]
        public double RowSpace { get; set; }

        [Expected("Population")]
        public double Population { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        [Expected("Experiment", "ExpID")]
        public virtual Experiment Experiment { get; set; }

        [Expected("Method", "PlantingMethod", "Planting Method")]
        public virtual Method Method { get; set; }
    }
}
