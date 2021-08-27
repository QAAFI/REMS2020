using Rems.Domain.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, true, "Design")]
    public class Design : IEntity, IComparable<Design>, IEquatable<Design>
    {
        public int DesignId { get; set; }

        public int LevelId { get; set; }

        public int TreatmentId { get; set; }

        [NotMapped]
        [Expected("Experiment", "ExpID")]
        public string Experiment { get; set; }

        [NotMapped]
        [Expected("Repetition", "Rep", "RepNo")]
        public int Repetition{ get; set; }

        [NotMapped]
        [Expected("Plot", "PlotID")]
        public string Plot { get; set; }

        [Expected("Treatment", "TreatmentName", "Treatment Name")]
        public virtual Treatment Treatment { get; set; }

        public virtual Level Level { get; set; }

        public int CompareTo(Design other)
        {
            int value = this.Treatment.Name.CompareTo(other.Treatment.Name);

            if (value == 0)
                return this.Level.Name.CompareTo(other.Level.Name);
            else
                return value;
        }

        public bool Equals(Design other)
            => this.Treatment?.Name == other.Treatment?.Name
            && this.Level.Name == other.Level.Name;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;            

            if (obj is null)
                return false;

            return Equals(obj as Design);
        }

        public override int GetHashCode() => DesignId;

        public static bool operator ==(Design left, Design right)
            => left is null ? right is null : left.Equals(right);        

        public static bool operator !=(Design left, Design right) 
            => !(left == right);        

        public static bool operator <(Design left, Design right)
            => left is null ? right is not null : left.CompareTo(right) < 0;        

        public static bool operator <=(Design left, Design right)
            => left is null || left.CompareTo(right) <= 0;        

        public static bool operator >(Design left, Design right)
            => left is not null && left.CompareTo(right) > 0;        

        public static bool operator >=(Design left, Design right)
            => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
