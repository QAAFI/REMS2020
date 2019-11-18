using System;


namespace Rems.Domain.Entities
{
    public class MetData : IEntity
    {
        public int MetStationId { get; set; }

        public int? TraitId { get; set; }

        public DateTime Date { get; set; }

        public double? Value { get; set; }


        public virtual MetStation MetStation { get; set; }
        public virtual Trait Trait { get; set; }


        
    }
}
