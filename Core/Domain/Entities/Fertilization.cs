using System;
using System.Collections.Generic;


namespace Rems.Domain.Entities
{
    public class Fertilization : ITreatment
    {
        public Fertilization()
        {
            FertilizationInfo = new HashSet<FertilizationInfo>();
        }

        public int FertilizationId { get; set; }

        public int TreatmentId { get; set; }

        public int? FertilizerId { get; set; }

        public int? MethodId { get; set; }

        public int? UnitId { get; set; }

        public DateTime Date { get; set; }        

        public int? Amount { get; set; }

        public int? Depth { get; set; }        

        public string Notes { get; set; }


        public virtual Fertilizer Fertilizer { get; set; }
        public virtual Method Method { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<FertilizationInfo> FertilizationInfo { get; set; }


    }
}
