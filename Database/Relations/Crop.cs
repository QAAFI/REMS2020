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

        // For use with Activator.CreateInstance()
        public Crop(
            int cropId,
            string name,
            string notes
        )
        {
            CropId = cropId;
            Name = name;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("CropId")]
        public int CropId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; }

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }

        [ForeignKey]
        public virtual ICollection<Experiment> Experiments { get; set; }
    }
}
