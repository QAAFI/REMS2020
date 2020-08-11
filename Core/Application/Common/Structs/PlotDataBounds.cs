using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common
{
    public struct PlotDataBounds
    {
        public DateTime XMin { get; set; }
        public DateTime XMax { get; set; }

        public double YMin { get; set; }
        public double YMax { get; set; }
    }
}
