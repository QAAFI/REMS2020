using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Trait : BaseEntity
    {
        public Trait() : base()
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

        public int UnitId { get; set; }

        public string Name { get; set; } = null;

        public string Type { get; set; } = null;

        public string Description { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual Unit DefaultUnit { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
        public virtual ICollection<SoilLayerData> SoilLayerData { get; set; }
        public virtual ICollection<SoilLayerTrait> SoilLayerTraits { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
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
