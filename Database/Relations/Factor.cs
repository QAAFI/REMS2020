using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Factor
    {
        public Factor()
        {
            Level = new HashSet<Level>();
        }

        public int FactorId { get; set; }
        public string FactorName { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Level> Level { get; set; }
    }
}
