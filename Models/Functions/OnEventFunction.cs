using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class OnEventFunction : Node
    {
        public string SetEvent { get; set; } = default;

        public string ReSetEvent { get; set; } = default;

        public OnEventFunction()
        { }
    }
}
