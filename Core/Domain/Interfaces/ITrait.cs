using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    public interface ITrait : IEntity
    {
        int TraitId { get; }
    }
}
