using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Field : INamed
    {
        public Field()
        {
            Experiments = new HashSet<Experiment>();
        }        

        public int FieldId { get; set; }

        public int SiteId { get; set; }

        public int SoilId { get; set; }

        public string Name { get; set; } = null;

        public double? Latitude { get; set; } = null;

        public double? Longitude { get; set; } = null;

        public double? Elevation { get; set; } = null;

        public double? Slope { get; set; } = null;

        public int? Depth { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual Site Site { get; set; }
        public virtual Soil Soil { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }


    }
}
