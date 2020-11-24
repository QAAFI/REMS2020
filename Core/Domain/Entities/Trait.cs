using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Trait : INamed
    {
        public Trait()
        {
            MetData = new HashSet<MetData>();
            PlotData = new HashSet<PlotData>();
            SoilData = new HashSet<SoilData>();
            SoilLayerData = new HashSet<SoilLayerData>();
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
            SoilTraits = new HashSet<SoilTrait>();
            Stats = new HashSet<Stat>();
        }

        public int TraitId { get; set; }

        public int? UnitId { get; set; }

        public string Name { get; set; } = null;

        public string Type { get; set; } = null;

        public string Description { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual Unit Unit { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
    }

    public static partial class Extensions
    {
        public static bool NameMatches(this Trait trait, string name)
        {
            if (trait.Name == name)
                return true;

            if (trait.Name.ToLower() == name.ToLower())
            {
                trait.Name = name;
                return true;
            }

            return false;
        }
    }
}
