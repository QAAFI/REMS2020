using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("ChemicalApplication")]
    public class ChemicalApplication
    {
        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("ApplicationId")]
        public int ApplicationId { get; set; }

        [Column("ApplicationDate")]
        public DateTime? ApplicationDate { get; set; }

        [Column("Target")]
        public string Target { get; set; }

        [Column("ActiveIngredient")]
        public int? ActiveIngredient { get; set; }

        [Column("Amount")]
        public double? Amount { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        public virtual Method ApplicationMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
