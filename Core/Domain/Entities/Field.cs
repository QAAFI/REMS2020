﻿using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Information)]
    public class Field : IEntity
    {
        public Field()
        {
            Experiments = new HashSet<Experiment>();
        }        

        public int FieldId { get; set; }

        public int SiteId { get; set; }

        public int SoilId { get; set; }

        [Expected("Name", "FieldName")]
        public string Name { get; set; }

        [Expected("Latitude", "Lat")]
        public double? Latitude { get; set; } = null;

        [Expected("Longitude", "Lon")]
        public double? Longitude { get; set; } = null;

        [Expected("Elevation")]
        public double? Elevation { get; set; } = null;

        [Expected("Slope")]
        public double? Slope { get; set; } = null;

        [Expected("Depth")]
        public int? Depth { get; set; } = null;

        [Expected("Notes")]
        public string Notes { get; set; } = null;

        [Expected("Site", "SiteName")]
        public virtual Site Site { get; set; }

        [Expected("Soil", "SoilType")]
        public virtual Soil Soil { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }


    }
}
