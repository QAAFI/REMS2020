using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("Unit")]
    public class Unit
    {
        public Unit()
        {
            ChemicalApplications = new HashSet<ChemicalApplication>();
            Fertilizations = new HashSet<Fertilization>();
            PlotData = new HashSet<PlotData>();
            Stats = new HashSet<Stat>();
            Traits = new HashSet<Trait>();
        }

        // For use with Activator.CreateInstance
        public Unit(
            double unitId,
            string name,
            string notes
        )
        {
            UnitId = (int)unitId;
            Name = name;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("UnitId")]
        public int UnitId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;

        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
        public virtual ICollection<Trait> Traits { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.UnitId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.UnitId)
                    .HasName("UnitId");

                // Define properties
                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(10);
            });
        }
    }
}
