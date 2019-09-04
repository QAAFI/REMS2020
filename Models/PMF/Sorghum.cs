using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF
{
    public class SorghumArbitrator : Node
    {
        public double DltTT { get; set; } = default;

        public double WatSupply { get; set; } = default;

        public double NMassFlowSuplpy { get; set; } = default;

        public double NDiffusionSupply { get; set; } = default;

        public double SWAvailRatio { get; set; } = default;

        public double PhotoStress { get; set; } = default;

        public double Avail { get; set; } = default;

        public double PotAvail { get; set; } = default;

        public double TotalAvail { get; set; } = default;

        public double TotalPotAvail { get; set; } = default;

        public SorghumArbitrator()
        { }
    }

    public class SorghumArbitratorN : Node
    {
        public SorghumArbitratorN()
        {
            Name = "NArbitrator";
        }
    }
}
