using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Fertilizer : BaseEntity
    {
        public Fertilizer() : base()
        {
            Fertilization = new HashSet<Fertilization>();
        }

        public int FertilizerId { get; set; }

        public string Name { get; set; }

        public double? Nitrogen { get; set; }

        public double? Phosphorus { get; set; }

        public double? Potassium { get; set; }

        public double? Calcium { get; set; }

        public double? Sulfur { get; set; }

        public double? Other { get; set; }

        public string OtherElements { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<Fertilization> Fertilization { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fertilizer>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FertilizerId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FertilizerId)
                    .HasName("FertilizerId")
                    .IsUnique();

                // Define the table properties
                entity.Property(e => e.FertilizerId)
                    .HasColumnName("FertilizerId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(60);

                entity.Property(e => e.Calcium)
                    .HasColumnName("Calcium")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Potassium)
                    .HasColumnName("Potassium")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Nitrogen)
                    .HasColumnName("Nitrogen")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Phosphorus)
                    .HasColumnName("Phosphorus")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sulfur)
                    .HasColumnName("Sulfur")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Other)
                    .HasColumnName("Other")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OtherElements)
                    .HasColumnName("OtherElements")
                    .HasMaxLength(20);

                entity.Property(e => e.Notes)
                    .HasColumnName("Notes")
                    .HasMaxLength(100);

            });

        }
    }
}
