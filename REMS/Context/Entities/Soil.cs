using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
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
            Type = soilType;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("SoilId")]
        public int SoilId { get; set; }

        [Nullable]
        [Column("SoilType")]
        public string Type { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Soil>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.SoilId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilId");

                // Define properties
                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilId");

                entity.Property(e => e.Type)
                    .HasColumnName("Type")
                    .HasMaxLength(30);
            });
        }
    }
}
