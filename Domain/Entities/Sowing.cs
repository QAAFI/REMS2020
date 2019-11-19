using System;

namespace Rems.Domain.Entities
{
    public class Sowing : IEntity
    {
        public int SowingId { get; set; }

        public int TreatmentId { get; set; }

        public DateTime? Date { get; set; }

        public string Cultivar { get; set; }
        public double? RowSpace { get; set; }
        public double? Depth { get; set; }
        public double? Population { get; set; }
        public double? FTN { get; set; }
        public string SkipConfig { get; set; } //single, double, skip

        public string Notes { get; set; }

        public virtual Treatment Treatment { get; set; }

       
    }
}
