using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    public interface ITreatment : IEntity
    {
        int TreatmentId { get; set; }
    }
}
