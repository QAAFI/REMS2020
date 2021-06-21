using Rems.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", true, "Experiments")]
    public class Experiment : IEntity
    {
        public Experiment()
        {
            ExperimentInfo = new HashSet<ExperimentInfo>();
            ResearcherList = new HashSet<ResearcherList>();
            Treatments = new HashSet<Treatment>();
        }

        public int ExperimentId { get; set; }

        public int CropId { get; set; }

        public int FieldId { get; set; }

        public int? MetStationId { get; set; }

        public int? MethodId { get; set; }

        [Expected("Name", "Experiment", "Exp Name")]
        public string Name { get; set; }

        [Expected("Description", "Exp Description")]
        public string Description { get; set; }

        [NotMapped]
        [Expected("Site", "SiteName", "Site Name")]
        public string SiteName { get; set; }

        [Expected("BeginDate", "Begin Date")]
        public DateTime BeginDate { get; set; }

        [Expected("EndDate", "End Date")]
        public DateTime EndDate { get; set; }

        [Expected("Design", "ExpDesign", "Exp Design")]
        public string Design { get; set; }

        [Expected("Repetitions", "Reps", "Number of Reps")]
        public int Repetitions { get; set; }

        [Expected("Rating")]
        public int Rating { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        [Expected("Crop")]
        public virtual Crop Crop { get; set; }

        [Expected("Field", "FieldName", "Field Name")]
        public virtual Field Field { get; set; }

        [Expected("MetStation", "MetStationName", "MetStation Name", "Station")]
        public virtual MetStation MetStation { get; set; }
                
        public virtual Method Method { get; set; }

        public virtual Sowing Sowing { get; set; }

        public virtual ICollection<ExperimentInfo> ExperimentInfo { get; set; }
        public virtual ICollection<ResearcherList> ResearcherList { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
