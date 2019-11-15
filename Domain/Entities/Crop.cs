using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Crop
    {
        public Crop()
        {
            Experiments = new HashSet<Experiment>();
        }

        public int CropId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }

        
    }
}
