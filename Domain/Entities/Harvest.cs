using System;

namespace Rems.Domain.Entities
{
    public class Harvest
    {
        public Harvest()
        { }

        public int HarvestId { get; set; }

        public int TreatmentId { get; set; }

        public int MethodId { get; set; }

        public DateTime? Date { get; set; }        

        public string Notes { get; set; }

        public virtual Method HarvestMethod { get; set; }
        public virtual Treatment Treatment { get; set; }


    }
}
