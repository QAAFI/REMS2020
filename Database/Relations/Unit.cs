using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
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
            string unitName,
            string notes
        )
        {
            UnitId = (int)unitId;
            UnitName = unitName;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("UnitId")]
        public int UnitId { get; set; }

        [Nullable]
        [Column("UnitName")]
        public string UnitName { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;

        public virtual ICollection<ChemicalApplication> ChemicalApplications { get; set; }
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<PlotData> PlotData { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
        public virtual ICollection<Trait> Traits { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.UnitId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.UnitId)
                    .HasName("UnitID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UnitName).HasMaxLength(10);
            });
        }
    }
}
