using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Experiment")]
    public class Experiment
    {
        public Experiment()
        {
            ExperimentInfo = new HashSet<ExperimentInfo>();
            ResearcherList = new HashSet<ResearcherList>();
            Treatments = new HashSet<Treatment>();
        }

        public Experiment(
            double experimentId,
            string experimentName
        )        
        {
            ExperimentId = (int)experimentId;
            ExperimentName = experimentName;            
        }

        [PrimaryKey]
        [Column("ExperimentId")]
        public int ExperimentId { get; set; }

        [Column("ExperimentName")]
        public string ExperimentName { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("CropId")]
        public int? CropId { get; set; }

        [Column("FieldId")]
        public int? FieldId { get; set; }

        [Column("BeginDate")]
        public DateTime? BeginDate { get; set; }

        [Column("EndDate")]
        public DateTime? EndDate { get; set; }

        [Column("MetStationId")]
        public int? MetStationId { get; set; }

        [Column("ExperimentDesign")]
        public string ExperimentDesign { get; set; }

        [Column("Repetitions")]
        public short? Repetitions { get; set; }

        [Column("Rating")]
        public int? Rating { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("PlantingNotes")]
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
