using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class Constant : ApsimNode
    {
        public double FixedValue { get; set; } = default;

        public string Units { get; set; } = default;

        public Constant()
        { }
    }
}
