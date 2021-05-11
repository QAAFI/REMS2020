using System;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Tillage : IEntity
    {
        public Tillage()
        {
            TillageInfo = new HashSet<TillageInfo>();
        }

        public int TillageId { get; set; }

        public int? TreatmentId { get; set; }

        public int? MethodId { get; set; }

        public DateTime Date { get; set; }        

        public double Depth { get; set; }

        public string Notes { get; set; }

        public virtual Method TillageMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<TillageInfo> TillageInfo { get; set; }

    }
}
