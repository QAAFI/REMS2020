using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class SoilNitrogen : Node
    {
        public List<string> fom_types { get; set; } = default;

        public List<double> fract_carb { get; set; } = default;

        public List<double> fract_cell { get; set; } = default;

        public List<double> fract_lign { get; set; } = default;

        public SoilNitrogen()
        { }
    }
}
