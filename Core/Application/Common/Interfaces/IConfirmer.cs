using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    public interface IConfirmer
    {
        bool Confirm(string message);
    }
}
