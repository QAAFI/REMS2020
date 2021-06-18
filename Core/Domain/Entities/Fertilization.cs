using Rems.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", false, "Fertilization")]
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

        [Expected("Date")]
        public DateTime Date { get; set; }

        [Expected("Amount")]
        public double Amount { get; set; }

        [Expected("Depth")]
        public int Depth { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        [NotMapped]
        [Expected("Experiment")]
        public Experiment Experiment { get; set; }

        [Expected("Fertilizer", "FertilizerName", "Fertilizer Name")]
        public virtual Fertilizer Fertilizer { get; set; }
        
        [Expected("Method", "Fertilization Method")]
        public virtual Method Method { get; set; }
        
        [Expected("Treatment", "TreatmentName", "Treatment Name")]
        public virtual Treatment Treatment { get; set; }

        [Expected("Unit", "Units")]
        public virtual Unit Unit { get; set; }

        public virtual ICollection<FertilizationInfo> FertilizationInfo { get; set; }
    }
}
