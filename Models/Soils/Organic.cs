using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class Organic : ApsimNode
    {
        public double FOMCNRatio { get; set; } = default;

        public List<double> Thickness { get; set; } = default;

        public List<double> Carbon { get; set; } = default;

        public List<double> SoilCNRatio { get; set; } = default;

        public List<double> FBiom { get; set; } = default;

        public List<double> FInert { get; set; } = default;

        public List<double> FOM { get; set; } = default;

        public int OCUnits { get; set; } = default;

        public string OCMetaData { get; set; } = default;

        public Organic()
        { }
    }
}
