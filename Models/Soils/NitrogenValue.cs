using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class NitrogenValue : ApsimNode
    {
        public List<double> Values { get; set; } = default;

        public bool StoredAsPPM { get; set; } = default;

        public NitrogenValue()
        { }
    }
}
