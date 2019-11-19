using System;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Experiment : IEntity
    {
        public Experiment()
        {
            ExperimentInfo = new HashSet<ExperimentInfo>();
            ResearcherList = new HashSet<ResearcherList>();
            Treatments = new HashSet<Treatment>();
        }

        public int ExperimentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? CropId { get; set; }

        public int? FieldId { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? MetStationId { get; set; }

        public string Design { get; set; }

        public int? Repetitions { get; set; }

        public int? Rating { get; set; }

        public string Notes { get; set; }

        public int? MethodId { get; set; }

        public string PlantingNotes { get; set; }


        public virtual Crop Crop { get; set; }
        public virtual Field Field { get; set; }
        public virtual MetStation MetStation { get; set; }
        public virtual Method Method { get; set; }

        public virtual ICollection<ExperimentInfo> ExperimentInfo { get; set; }
        public virtual ICollection<ResearcherList> ResearcherList { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }

    }
}
