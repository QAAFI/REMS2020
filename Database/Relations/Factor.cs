using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Factor")]
    public class Factor
    {
        public Factor()
        {
            Level = new HashSet<Level>();
        }

        [Column("FactorId")]
        public int FactorId { get; set; }

        [Column("FactorName")]
        public string FactorName { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }


        public virtual ICollection<Level> Level { get; set; }
    }
}
