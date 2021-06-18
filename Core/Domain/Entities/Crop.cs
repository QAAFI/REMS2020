using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Information, "Crops")]
    public class Crop : IEntity
    {
        public Crop()
        {
            Experiments = new HashSet<Experiment>();
        }

        public int CropId { get; set; }

        [Expected("Name", "Crop", "Crop Name")]
        public string Name { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }        
    }
}
