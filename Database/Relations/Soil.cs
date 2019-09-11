using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("Soil")]
    public class Soil
    {
        public Soil()
        {
            Fields = new HashSet<Field>();
            SoilLayers = new HashSet<SoilLayer>();
            SoilTraits = new HashSet<SoilTrait>();
        }

        // For use with Activator.CreateInstance
        public Soil(
            double soilId,
            string soilType,
            string notes
        )
        {
            SoilId = (int)soilId;
            SoilType = soilType;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("SoilId")]
        public int SoilId { get; set; }

        [Nullable]
        [Column("SoilType")]
        public string SoilType { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Soil>(entity =>
            {
                entity.HasKey(e => e.SoilId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilID586");

                entity.Property(e => e.SoilId).HasColumnName("SoilID");

                entity.Property(e => e.SoilType).HasMaxLength(30);
            });
        }
    }
}
