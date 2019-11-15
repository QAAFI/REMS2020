using System;

namespace Rems.Domain.Entities
{
    public class Stat
    {
        public int StatId { get; set; }

        public int? TreatmentId { get; set; }

        public int? TraitId { get; set; }

        public int? UnitId { get; set; }

        public DateTime? Date { get; set; }

        public double? Mean { get; set; }

        public double? SE { get; set; }

        public int? Number { get; set; }
        

        public virtual Trait Trait { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }


    }
}
