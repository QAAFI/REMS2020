using System;
using Rems.Domain.Attributes;

namespace Rems.Domain.Entities
{
    public class PlotData : ITrait
    {
        public int PlotDataId { get; set; }

        public int? PlotId { get; set; }

        public int TraitId { get; set; }

        [Graphable]
        public DateTime? Date { get; set; }

        public string Sample { get; set; }

        [Graphable]
        public double? Value { get; set; }

        public int? UnitId { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }
        public virtual Unit Unit { get; set; }
        
    }
}
