using System;

namespace Rems.Domain.Entities
{
    public class SoilLayerData : ITrait
    {
        public int SoilLayerDataId { get; set; }

        public int PlotId { get; set; }

        public int TraitId { get; set; }

        public DateTime Date { get; set; }

        public int DepthFrom { get; set; }

        public int DepthTo { get; set; }

        public double Value { get; set; }


        public virtual Plot Plot { get; set; }
        public virtual Trait Trait { get; set; }

    }
}
