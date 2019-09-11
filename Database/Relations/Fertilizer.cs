using System;
using System.Collections.Generic;

namespace Database
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
            Phosphorous = phosphorous;
            Potassium = potassium;
            Calcium = calcium;
            Sulfur = sulfur;
            OtherAmount = otherAmount;
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
        public double? Phosphorous { get; set; }

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
        public double? OtherAmount { get; set; }

        [Nullable]
        [Column("OtherElements")]
        public string OtherElements { get; set; }

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }


        public virtual ICollection<Fertilization> Fertilization { get; set; }
    }
}
