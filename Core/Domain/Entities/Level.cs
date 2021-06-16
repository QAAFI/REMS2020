using Rems.Domain.Attributes;
using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    [ExcelSource(RemsSource.Information)]
    public class Level : IEntity
    {
        public Level()
        {
            Designs = new HashSet<Design>();
        }

        public int LevelId { get; set; }

        public int FactorId { get; set; }

        [Expected("Name", "Level")]
        public string Name { get; set; }

        [Expected("Notes")]
        public string Notes { get; set; }

        public string Specification { get; set; }

        [Expected("Factor")]
        public virtual Factor Factor { get; set; }

        public virtual ICollection<Design> Designs { get; set; }

    }
}
