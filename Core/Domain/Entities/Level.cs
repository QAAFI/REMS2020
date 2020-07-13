using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Level : INamed
    {
        public Level()
        {
            Designs = new HashSet<Design>();
        }

        public int LevelId { get; set; }

        public int? FactorId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        public virtual Factor Factor { get; set; }
        public virtual ICollection<Design> Designs { get; set; }

    }
}
