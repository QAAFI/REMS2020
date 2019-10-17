using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class LinearInterpolationFunction : ApsimNode
    {
        public string XProperty { get; set; } = default;
        
        public LinearInterpolationFunction()
        { }
    }
}
