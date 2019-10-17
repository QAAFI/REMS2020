using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class Sample : ApsimNode
    {
        public List<double> Thickness { get; set; } = default;

        public NitrogenValue NO3N { get; set; } = default;

        public NitrogenValue NH4N { get; set; } = default;

        public List<double> SW { get; set; } = default;

        public List<double> OC { get; set; } = default;

        public List<double> EC { get; set; } = default;

        public List<double> CL { get; set; } = default;

        public List<double> ESP { get; set; } = default;

        public List<double> PH { get; set; } = default;

        public int SWUnits { get; set; } = default;

        public int OCUnits { get; set; } = default;

        public int PHUnits { get; set; } = default;

        public Sample()
        { }
    }
}
