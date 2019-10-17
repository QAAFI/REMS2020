using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Functions
{
    public class PowerFunction : ApsimNode
    {
        public double? Exponent { get; set; } = default;

        public PowerFunction()
        { }
    }
}
