using System;
using System.Collections.Generic;

namespace Database
{
    [Relation("Level")]
    public class Level
    {
        public Level()
        {
            Designs = new HashSet<Design>();
        }

        public int LevelId { get; set; }
        public int? FactorId { get; set; }
        public string LevelName { get; set; }
        public string Notes { get; set; }

        public virtual Factor Factor { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
    }
}
