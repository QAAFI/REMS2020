using System;

namespace Rems.Domain.Entities
{
    public class Sowing : IEntity
    {
        public int SowingId { get; set; }

        public int ExperimentId { get; set; }

        public int MethodId { get; set; }

        public DateTime Date { get; set; }

        public string Cultivar { get; set; }
        
        public double? Depth { get; set; }
        public double? RowSpace { get; set; }
        public double? Population { get; set; }

        public string Notes { get; set; }

        public virtual Method Method { get; set; }
        public virtual Experiment Experiment { get; set; }
    }
}
