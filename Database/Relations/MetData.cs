using System;
using System.Collections.Generic;

namespace Database
{
    public partial class MetData
    {
        public int MetStationId { get; set; }
        public int TraitId { get; set; }
        public DateTime Date { get; set; }
        public double? Value { get; set; }

        public virtual MetStations MetStation { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
