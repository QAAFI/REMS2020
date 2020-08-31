using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Plot : IEntity
    {
        public Plot()
        {
            PlotData = new HashSet<PlotData>();
            SoilData = new HashSet<SoilData>();
            SoilLayerData = new HashSet<SoilLayerData>();
        }

        public int PlotId { get; set; }

        public int TreatmentId { get; set; }

        public int Repetition { get; set; }

        public int? Column { get; set; }

        public int? Rows { get; set; }


        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }

    }
}
