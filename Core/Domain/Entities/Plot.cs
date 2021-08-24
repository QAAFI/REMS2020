using System;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Plot : IEntity, IComparable<Plot>, IEquatable<Plot>
    {
        public Plot()
        {
            PlotData = new HashSet<PlotData>();
            SoilData = new HashSet<SoilData>();
            SoilLayerData = new HashSet<SoilLayerData>();
        }

        public int PlotId { get; set; }

        public int TreatmentId { get; set; }

        public int Repetition { get; set; }

        public int? Column { get; set; }

        public int? Rows { get; set; }


        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }

        public int CompareTo(Plot other)
        {
            int value = this.Treatment.Name.CompareTo(other.Treatment.Name);

            if (value == 0)
                return this.Repetition.CompareTo(other.Repetition);
            else
                return value;
        }

        public bool Equals(Plot other)
            => this.Treatment?.Name == other.Treatment?.Name
            && this.Repetition == other.Repetition
            && this.Column == other.Column;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null)
                return false;

            return Equals(obj as Plot);
        }

        public override int GetHashCode() => PlotId;

        public static bool operator ==(Plot left, Plot right)
            => left is null ? right is null : left.Equals(right);

        public static bool operator !=(Plot left, Plot right)
            => !(left == right);

        public static bool operator <(Plot left, Plot right)
            => left is null ? right is not null : left.CompareTo(right) < 0;        

        public static bool operator <=(Plot left, Plot right)
            => left is null || left.CompareTo(right) <= 0;

        public static bool operator >(Plot left, Plot right)
            => left is not null && left.CompareTo(right) > 0;        

        public static bool operator >=(Plot left, Plot right)
            => left is null ? right is null : left.CompareTo(right) >= 0;        
    }
}
