using Rems.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, false, "Tillage")]
    public class Tillage : IEntity
    {
        public Tillage()
        {
            TillageInfo = new HashSet<TillageInfo>();
        }

        public int TillageId { get; set; }

        public int? TreatmentId { get; set; }

        public int? MethodId { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        [Expected("Depth")]
        public double Depth { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        [NotMapped]
        [Expected("Experiment", "ExpID")]
        public Experiment Experiment { get; set; }

        [Expected("Method", "Tillage Method")]
        public virtual Method Method { get; set; }

        [Expected("Treatment", "TreatmentName", "Treatment Name")]
        public virtual Treatment Treatment { get; set; }
        
        public virtual ICollection<TillageInfo> TillageInfo { get; set; }
    }
}
