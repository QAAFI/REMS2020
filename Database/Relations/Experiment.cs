using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Experiment
    {
        public Experiment()
        {
            ExperimentInfo = new HashSet<ExperimentInfo>();
            ResearcherList = new HashSet<ResearcherList>();
            Treatments = new HashSet<Treatment>();
        }

        public int ExperimentId { get; set; }
        public string ExperimentName { get; set; }
        public string Description { get; set; }
        public int? CropId { get; set; }
        public int? FieldId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MetStationId { get; set; }
        public string ExperimentDesign { get; set; }
        public short? Repetitions { get; set; }
        public int? Rating { get; set; }
        public string Notes { get; set; }
        public int? MethodId { get; set; }
        public string PlantingNotes { get; set; }

        public virtual Crop Crop { get; set; }
        public virtual Field Field { get; set; }
        public virtual MetStations MetStation { get; set; }
        public virtual Method PlantingMethod { get; set; }

        public virtual ICollection<ExperimentInfo> ExperimentInfo { get; set; }
        public virtual ICollection<ResearcherList> ResearcherList { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
