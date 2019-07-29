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

        public int FertilizerId { get; set; }
        public string FertilizerName { get; set; }
        public double? Nitrogen { get; set; }
        public double? Phosphorous { get; set; }
        public double? Potassium { get; set; }
        public double? Calcium { get; set; }
        public double? Sulfur { get; set; }
        public double? OtherAmount { get; set; }
        public string OtherElements { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Fertilization> Fertilization { get; set; }
    }
}
