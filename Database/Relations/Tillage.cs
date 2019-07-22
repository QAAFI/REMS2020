using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Tillage
    {
        public Tillage()
        {
            TillageInfo = new HashSet<TillageInfo>();
        }

        public int? TreatmentId { get; set; }
        public int TillageId { get; set; }
        public DateTime? TillageDate { get; set; }
        public int? TillageMethodId { get; set; }
        public double? TillageDepth { get; set; }
        public string TillageNotes { get; set; }

        public virtual Method TillageMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<TillageInfo> TillageInfo { get; set; }
    }
}
