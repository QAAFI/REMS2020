using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class SoilWater : Node
    {
        public string SummerDate = default;

        public double SummerU = default;

        public double SummerCona = default;

        public string WinterDate = default;

        public double WinterU = default;

        public double WinterCona = default;

        public double DiffusConst = default;

        public double DiffusSlope = default;

        public double Salb = default;

        public double CN2Bare = default;

        public double CNRed = default;

        public double CNCov = default;

        public double slope = default;

        public double discharge_width = default;

        public double catchment_area = default;

        public double max_pond = default;

        public List<double> Thickness { get; set; } = default;

        public List<double> SWCON { get; set; } = default;

        public List<double> KLAT { get; set; } = default;

        public double ResidueInterception { get; set; } = default;

        public SoilWater()
        { }
    }
}
