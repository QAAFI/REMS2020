using Rems.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, false, "Irrigation", "Irrigations")]
    public class Irrigation : ITreatment, IComparable<Irrigation>, IEquatable<Irrigation>
    {
        public Irrigation()
        {
            IrrigationInfo = new HashSet<IrrigationInfo>();
        }

        public int IrrigationId { get; set; }

        public int? MethodId { get; set; }

        public int TreatmentId { get; set; }

        [Expected("Date")]
        public DateTime Date { get; set; }

        [Expected("Amount")]
        public double Amount { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        [NotMapped]
        [Expected("Experiment", "ExpID")]
        public Experiment Experiment { get; set; }

        [Expected("Method", "Irrigation Method")]
        public virtual Method Method { get; set; }
        
        [Expected("Treatment", "TreatmentName", "Treatment Name")]
        public virtual Treatment Treatment { get; set; }

        public virtual ICollection<IrrigationInfo> IrrigationInfo { get; set; }

        public int CompareTo(Irrigation other)
            => Date.CompareTo(other.Date) is int i && i != 0
                ? i : Amount.CompareTo(other.Amount);

        public bool Equals(Irrigation other)
            => Date == other.Date
            && Amount == other.Amount;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null)
                return false;

            return Equals(obj as Irrigation);
        }

        public override int GetHashCode() => IrrigationId;

        public static bool operator ==(Irrigation left, Irrigation right)
            => left is null ? right is null : left.Equals(right);

        public static bool operator !=(Irrigation left, Irrigation right)
            => !(left == right);

        public static bool operator <(Irrigation left, Irrigation right)
            => left is null ? right is not null : left.CompareTo(right) < 0;

        public static bool operator <=(Irrigation left, Irrigation right)
            => left is null || left.CompareTo(right) <= 0;

        public static bool operator >(Irrigation left, Irrigation right)
            => left is not null && left.CompareTo(right) > 0;

        public static bool operator >=(Irrigation left, Irrigation right)
            => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
