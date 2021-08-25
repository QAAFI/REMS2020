using Rems.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, false, "Tillage", "Tillages")]
    public class Tillage : IEntity, IComparable<Tillage>, IEquatable<Tillage>
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

        public int CompareTo(Tillage other)
            => Date.CompareTo(other.Date) is int i && i != 0
                ? i : Depth.CompareTo(other.Depth);

        public bool Equals(Tillage other)
            => Date == other.Date
            && Depth == other.Depth;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null)
                return false;

            return Equals(obj as Tillage);
        }

        public override int GetHashCode() => TillageId;

        public static bool operator ==(Tillage left, Tillage right)
            => left is null ? right is null : left.Equals(right);

        public static bool operator !=(Tillage left, Tillage right)
            => !(left == right);

        public static bool operator <(Tillage left, Tillage right)
            => left is null ? right is not null : left.CompareTo(right) < 0;

        public static bool operator <=(Tillage left, Tillage right)
            => left is null || left.CompareTo(right) <= 0;

        public static bool operator >(Tillage left, Tillage right)
            => left is not null && left.CompareTo(right) > 0;

        public static bool operator >=(Tillage left, Tillage right)
            => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
