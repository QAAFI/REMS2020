using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
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
            Stats = new HashSet<Stat>();
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
        public virtual ICollection<Stat> Stats { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trait>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.TraitId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.UnitId)
                    .HasName("TraitUnitId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitId");

                // Define properties
                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitId");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitId");

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(60);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(25);

                entity.Property(e => e.Type)
                    .HasColumnName("Type")
                    .HasMaxLength(10);

                // Define constraints
                entity.HasOne(d => d.DefaultUnit)
                    .WithMany(p => p.Traits)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitUnitId");
            });

        }
    }
}
