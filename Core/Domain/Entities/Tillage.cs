using Rems.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Experiments)]
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
        [Expected("Experiment")]
        public Experiment Experiment { get; set; }

        [Expected("Method")]
        public virtual Method Method { get; set; }

        [Expected("Treatment")]
        public virtual Treatment Treatment { get; set; }
        
        public virtual ICollection<TillageInfo> TillageInfo { get; set; }

    }
}
