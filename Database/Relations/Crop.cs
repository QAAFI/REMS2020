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

        [Column("CropId")]
        public int CropId { get; set; }

        [Column("CropName")]
        public string CropName { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
    }
}
