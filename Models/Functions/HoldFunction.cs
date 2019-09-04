using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class HoldFunction : Node
    {
        public string WhenToHold { get; set; } = default;

        public HoldFunction()
        { }
    }
}
