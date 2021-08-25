using Rems.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, false, "Fertilization", "Fertilizations")]
    public class Fertilization : ITreatment, IComparable<Fertilization>, IEquatable<Fertilization>
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
        [Expected("Experiment", "ExpID")]
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

        public int CompareTo(Fertilization other)
            => Date.CompareTo(other.Date) is int i && i != 0
                ? i : Amount.CompareTo(other.Amount) is int j && j != 0
                ? j : Depth.CompareTo(other.Depth);

        public bool Equals(Fertilization other)
            => Date == other.Date
            && Amount == other.Amount
            && Depth == other.Depth;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;            

            if (obj is null)            
                return false;            

            return Equals(obj as Fertilization);
        }

        public override int GetHashCode() => FertilizationId;

        public static bool operator ==(Fertilization left, Fertilization right)
            => left is null ? right is null : left.Equals(right);

        public static bool operator !=(Fertilization left, Fertilization right)
            => !(left == right);

        public static bool operator <(Fertilization left, Fertilization right)
            => left is null ? right is not null : left.CompareTo(right) < 0;

        public static bool operator <=(Fertilization left, Fertilization right)
            => left is null || left.CompareTo(right) <= 0;

        public static bool operator >(Fertilization left, Fertilization right)
            => left is not null && left.CompareTo(right) > 0;

        public static bool operator >=(Fertilization left, Fertilization right)
            => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
