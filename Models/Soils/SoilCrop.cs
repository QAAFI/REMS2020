using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class SoilCrop : Node
    {
        public List<double> LL { get; set; } = default;

        public List<double> KL { get; set; } = default;

        public List<double> XF { get; set; } = default;

        public string LLMetaData { get; set; } = default;

        public string KLMetaData { get; set; } = default;

        public string XFMetaData { get; set; } = default;

        public SoilCrop()
        { }
    }
}
