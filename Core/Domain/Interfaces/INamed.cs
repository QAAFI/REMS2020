using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Domain.Entities
{
    public interface INamed : IEntity
    {
        string Name { get; set; }
    }
}
