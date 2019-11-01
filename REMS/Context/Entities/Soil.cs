using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Soil : BaseEntity
    {
        public Soil() : base()
        {
            Fields = new HashSet<Field>();
            SoilLayers = new HashSet<SoilLayer>();
            SoilTraits = new HashSet<SoilTrait>();
        }

        public int SoilId { get; set; }

        public string Type { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<SoilLayer> SoilLayers { get; set; }
        public virtual ICollection<SoilTrait> SoilTraits { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
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
