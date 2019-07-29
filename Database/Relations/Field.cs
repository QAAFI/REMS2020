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

        public int FieldId { get; set; }
        public int? SiteId { get; set; }
        public int? SoilId { get; set; }
        public string FieldName { get; set; }
        public double? FieldLatitude { get; set; }
        public double? FieldLongitude { get; set; }
        public double? FieldElevation { get; set; }
        public double? Slope { get; set; }
        public int? Depth { get; set; }
        public string Notes { get; set; }

        public virtual Site Site { get; set; }
        public virtual Soil Soil { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }
    }
}
