using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    [Relation("Fertilizer")]
    public class Fertilizer
    {
        public Fertilizer()
        {
            Fertilization = new HashSet<Fertilization>();
        }

        public Fertilizer(
            double fertilizerId,
            string name,
            double nitrogen,
            double phosphorous,
            double potassium,
            double calcium,
            double sulfur,
            double otherAmount,
            string otherElements,
            string notes
        )
        {
            FertilizerId = (int)fertilizerId;
            Name = name;
            Nitrogen = nitrogen;
            Phosphorus = phosphorous;
            Potassium = potassium;
            Calcium = calcium;
            Sulfur = sulfur;
            Other = otherAmount;
            OtherElements = otherElements;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("FertilizerId")]
        public int FertilizerId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; }

        [Nullable]
        [Column("Nitrogen")]
        public double? Nitrogen { get; set; }

        [Nullable]
        [Column("Phosphorous")]
        public double? Phosphorus { get; set; }

        [Nullable]
        [Column("Potassium")]
        public double? Potassium { get; set; }

        [Nullable]
        [Column("Calcium")]
        public double? Calcium { get; set; }

        [Nullable]
        [Column("Sulfur")]
        public double? Sulfur { get; set; }

        [Nullable]
        [Column("OtherAmount")]
        public double? Other { get; set; }

        [Nullable]
        [Column("OtherElements")]
        public string OtherElements { get; set; }

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }


        public virtual ICollection<Fertilization> Fertilization { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
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
