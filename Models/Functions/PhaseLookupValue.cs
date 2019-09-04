using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class PhaseLookupValue : Node
    {
        public string Start { get; set; } = default;

        public string End { get; set; } = default;

        public PhaseLookupValue()
        { }
    }
}
