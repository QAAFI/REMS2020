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

        [PrimaryKey]
        [Column("LevelId")]
        public int LevelId { get; set; }

        [Column("FactorId")]
        public int? FactorId { get; set; }

        [Column("LevelName")]
        public string LevelName { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }


        public virtual Factor Factor { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
    }
}
