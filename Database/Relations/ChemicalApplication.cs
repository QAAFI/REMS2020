using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("ChemicalApplication")]
    public class ChemicalApplication
    {
        public ChemicalApplication()
        { }

        // For use with Activator.CreateInstance()
        public ChemicalApplication(
            int applicationId,
            int? treatmentId,            
            DateTime? applicationDate,
            string target,
            int? activeIngredient,
            double? amount,
            int? unitId,
            int? methodId,
            string notes
        )
        {
            TreatmentId = treatmentId;
            ApplicationId = applicationId;
            ApplicationDate = applicationDate;
            Target = target;
            ActiveIngredient = activeIngredient;
            Amount = amount;
            UnitId = unitId;
            MethodId = methodId;
            Notes = notes;
        }        

        [PrimaryKey]
        [Column("ApplicationId")]
        public int ApplicationId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

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

        [ForeignKey]
        public virtual Method ApplicationMethod { get; set; }

        [ForeignKey]
        public virtual Treatment Treatment { get; set; }

        [ForeignKey]
        public virtual Unit Unit { get; set; }
    }
}
