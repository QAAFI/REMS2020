using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Field")]
    public class Field
    {
        public Field()
        {
            Experiments = new HashSet<Experiment>();
        }

        // For use with Activator.CreateInstance
        public Field(
            double fieldId,
            double siteId,
            double soilId,
            string name,
            double? latitude,
            double? longitude,
            double? elevation,
            double? slope,
            int? depth,
            string notes
        )
        {
            FieldId = (int)fieldId;
            SiteId = (int)siteId;
            SoilId = (int)soilId;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
            Slope = slope;
            Depth = depth;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("FieldId")]
        public int FieldId { get; set; }

        [Column("SiteId")]
        public int SiteId { get; set; }

        [Column("SoilId")]
        public int SoilId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; } = null;

        [Nullable]
        [Column("Latitude")]
        public double? Latitude { get; set; } = null;

        [Nullable]
        [Column("Longitude")]
        public double? Longitude { get; set; } = null;

        [Nullable]
        [Column("Elevation")]
        public double? Elevation { get; set; } = null;

        [Nullable]
        [Column("Slope")]
        public double? Slope { get; set; } = null;

        [Nullable]
        [Column("Depth")]
        public int? Depth { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual Site Site { get; set; }
        public virtual Soil Soil { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }
    }
}
