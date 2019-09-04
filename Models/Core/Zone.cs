using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    public class Zone : Node
    {
        public double Area { get; set; } = default;

        public double Slope { get; set; } = default;

        public Zone()
        { }
    }
}
