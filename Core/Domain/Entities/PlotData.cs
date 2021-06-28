using Rems.Domain.Attributes;
using Rems.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Data", 1, false, "HarvestData", "PlotData")]
    public class PlotData : IEntity, IValue
    {
        public int PlotDataId { get; set; }

        public int PlotId { get; set; }

        public int TraitId { get; set; }

        [NotMapped]
        [Expected("Experiment")]
        public string Experiment { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        [Expected("Sample")]
        public string Sample { get; set; }

        public double Value { get; set; }

        public int? UnitId { get; set; }

        [Expected("Plot", "PlotID", "PlotId")]
        public virtual Plot Plot { get; set; }

        public virtual Trait Trait { get; set; }

        public virtual Unit Unit { get; set; }
        
    }
}
