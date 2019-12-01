using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    class TreatmentView : IEntity
    {
        public int TreatmentViewId { get; set; }
        public string Name { get; set; }
    }
}
