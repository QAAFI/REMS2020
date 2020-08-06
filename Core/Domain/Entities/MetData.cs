﻿using System;
using Rems.Domain.Attributes;

namespace Rems.Domain.Entities
{
    public class MetData : ITrait
    {
        public int MetStationId { get; set; }

        public int TraitId { get; set; }

        [Graphable]
        public DateTime Date { get; set; }

        [Graphable]
        public double? Value { get; set; }


        public virtual MetStation MetStation { get; set; }
        public virtual Trait Trait { get; set; }


        
    }
}