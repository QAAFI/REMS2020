using System.Collections.Generic;

namespace Rems.Domain.Entities
{
    public class Factor : INamed
    {
        public Factor()
        {
            Level = new HashSet<Level>();
        }

        public int FactorId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<Level> Level { get; set; }

    }
}
