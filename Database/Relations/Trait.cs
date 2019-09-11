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

        // For use with Activator.CreateInstance
        public Trait(
            double traitId,
            double unitId,
            string name,
            string type,
            string description,
            string notes
        )
        {
            TraitId = (int)traitId;
            UnitId = (int)unitId;
            Name = name;
            Type = type;
            Description = description;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("TraitId")]
        public int TraitId { get; set; }

        [Column("UnitId")]
        public int UnitId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; } = null;

        [Nullable]
        [Column("Type")]
        public string Type { get; set; } = null;

        [Nullable]
        [Column("Description")]
        public string Description { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


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
