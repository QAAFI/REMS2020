using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelFormat("Information", "Levels", "Factors")]
    public class Level : IEntity
    {
        public Level()
        {
            Designs = new HashSet<Design>();
        }

        public int LevelId { get; set; }

        public int FactorId { get; set; }

        [Expected("Name", "Level", "Level Name")]
        public string Name { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        public string Specification { get; set; }

        [Expected("Factor", "Factor Name")]
        public virtual Factor Factor { get; set; }

        public virtual ICollection<Design> Designs { get; set; }

    }
}
