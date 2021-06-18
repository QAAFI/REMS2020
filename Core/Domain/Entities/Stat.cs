using Rems.Domain.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Data", "Stats")]
    public class Stat : IEntity
    {
        public int StatId { get; set; }

        public int TreatmentId { get; set; }

        public int TraitId { get; set; }

        public int UnitId { get; set; }

        [NotMapped]
        [Expected("Experiment")]
        public string Experiment { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        [Expected("Mean")]
        public double Mean { get; set; }

        [Expected("SE")]
        public double SE { get; set; }

        [Expected("n", "N", "Number", "#")]
        public int Number { get; set; }

        [Expected("Trait")]
        public virtual Trait Trait { get; set; }

        [Expected("Treatment")]
        public virtual Treatment Treatment { get; set; }

        [Expected("Units", "Unit")]
        public virtual Unit Unit { get; set; }
    }
}
