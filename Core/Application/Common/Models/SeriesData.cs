using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common
{
    public class SeriesData<TX, TY>
    {
        public TX[] X { get; set; }
        public TY[] Y { get; set; }

        public string Name { get; set; }
        public int Series { get; set; }
        public string XName { get; set; }
        public string XUnits { get; set; }
        public string YName { get; set; }
        public string YUnits { get; set; }
    }
}
