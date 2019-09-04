using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class XYPairs : Node
    {
        public List<double> X { get; set; } = default;

        public List<double> Y { get; set; } = default;

        public XYPairs()
        { }
    }
}
