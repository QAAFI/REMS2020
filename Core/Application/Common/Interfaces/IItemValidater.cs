using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IItemValidater
    {
        string Name { get; set; }

        string Values { get; set; }

        string Item { get; set; }

        bool IsValid { get; set; }
    }
}
