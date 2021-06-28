using Rems.Domain.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Experiments", 1, true, "Design")]
    public class Design : IEntity
    {
        public int DesignId { get; set; }

        public int? LevelId { get; set; }

        public int? TreatmentId { get; set; }

        [NotMapped]
        [Expected("Experiment")]
        public string Experiment { get; set; }

        [NotMapped]
        [Expected("Repetition", "Rep", "RepNo")]
        public int Repetition{ get; set; }

        [NotMapped]
        [Expected("Plot")]
        public string Plot { get; set; }

        [Expected("Treatment", "TreatmentName", "Treatment Name")]
        public virtual Treatment Treatment { get; set; }

        public virtual Level Level { get; set; }
    }
}
