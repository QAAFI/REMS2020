using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Design")]
    public class Design
    {
        public Design()
        { }

        // For use with Activator.CreateInstance()
        public Design(
            int designId,
            int? levelId,
            int? treatmentId
        )
        {
            DesignId = designId;
            LevelId = levelId;
            TreatmentId = treatmentId;
        }

        [PrimaryKey]
        [Column("DesignId")]
        public int DesignId { get; set; }

        [Column("LevelId")]
        public int? LevelId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [ForeignKey]
        public virtual Level Level { get; set; }

        [ForeignKey]
        public virtual Treatment Treatment { get; set; }
    }
}
