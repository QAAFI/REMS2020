using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class SoilOrganicMatter : Node
    {
        public double RootCN { get; set; } = default;

        public double EnrACoeff { get; set; } = default;

        public double EnrBCoeff { get; set; } = default;

        public List<double> Thickness { get; set; } = default;

        public List<double> OC { get; set; } = default;

        public List<double> SoilCN { get; set; } = default;

        public List<double> FBiom { get; set; } = default;

        public List<double> FInert { get; set; } = default;

        public List<double> RootWt { get; set; } = default;

        public int OCUnits { get; set; } = default;

        public string OCMetaData { get; set; } = default;

        public SoilOrganicMatter()
        { }
    }
}
