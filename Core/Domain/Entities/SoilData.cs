using Rems.Domain.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Data", 1, false, "SoilData")]
    public class SoilData : IEntity
    {
        public int SoilDataId { get; set; }

        public int PlotId { get; set; }

        public int TraitId { get; set; }

        [NotMapped]
        [Expected("Experiment", "ExpID")]
        public string Experiment { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        public double Value { get; set; }

        [Expected("Plot", "PlotID")]
        public virtual Plot Plot { get; set; }

        public virtual Trait Trait { get; set; }
    }
}
