using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Information", true, "Fertilizers")]
    public class Fertilizer : IEntity
    {
        public Fertilizer()
        {
            Fertilization = new HashSet<Fertilization>();
        }

        public int FertilizerId { get; set; }

        [Expected("Name", "Fertiliser", "Fertilizer", "Fertilizer Name")]
        public string Name { get; set; }

        [Expected("Nitrogen", "N%")]
        public double? Nitrogen { get; set; } = null;

        [Expected("Phosphorus", "P%")]
        public double? Phosphorus { get; set; } = null;

        [Expected("Potassium", "K%")]
        public double? Potassium { get; set; } = null;

        [Expected("Calcium", "Ca%")]
        public double? Calcium { get; set; } = null;

        [Expected("Sulfur", "S%")]
        public double? Sulfur { get; set; } = null;

        [Expected("OtherPercent", "Other%")]
        public double? OtherPercent { get; set; } = null;

        [Expected("Other")]
        public string Other { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }


        public virtual ICollection<Fertilization> Fertilization { get; set; }

    }
}
