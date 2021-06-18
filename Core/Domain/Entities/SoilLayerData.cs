using Rems.Domain.Attributes;
using Rems.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Data", "SoilLayerData")]
    public class SoilLayerData : IEntity, IValue
    {
        public int SoilLayerDataId { get; set; }

        public int PlotId { get; set; }

        public int TraitId { get; set; }

        [NotMapped]
        [Expected("Experiment")]
        public string Experiment { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        [Expected("DepthFrom")]
        public int DepthFrom { get; set; }

        [Expected("DepthTo")]
        public int DepthTo { get; set; }

        public double Value { get; set; }

        [Expected("Plot")]
        public virtual Plot Plot { get; set; }

        public virtual Trait Trait { get; set; }

    }
}
