using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Crop")]
    public class Crop
    {
        public Crop()
        {
            Experiments = new HashSet<Experiment>();
        }

        public int CropId { get; set; }
        public string CropName { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
    }
}
