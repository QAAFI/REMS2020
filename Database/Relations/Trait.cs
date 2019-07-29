using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Trait")]
    public class Trait
    {
        public Trait()
        {
            MetData = new HashSet<MetData>();
            PlotData = new HashSet<PlotData>();
            SoilData = new HashSet<SoilData>();
            SoilLayerData = new HashSet<SoilLayerData>();
            SoilLayerTraits = new HashSet<SoilLayerTrait>();
            SoilTraits = new HashSet<SoilTrait>();
            Stats = new HashSet<Stats>();
        }
        
        public int TraitId { get; set; }
        public string TraitName { get; set; }
        public string TraitType { get; set; }
        public string Description { get; set; }
        public int? DefaultUnitId { get; set; }
        public string TraitNotes { get; set; }

        public virtual Unit DefaultUnit { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }
        public virtual ICollection<Stats> Stats { get; set; }
    }
}
