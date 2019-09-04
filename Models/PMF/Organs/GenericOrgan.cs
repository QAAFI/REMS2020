using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF.Organs
{
    public class GenericOrgan : Node
    {
        public bool StartLive { get; set; } = default;

        public double DMSupply { get; set; } = default;

        public double NSupply { get; set; } = default;

        public double DMDemand { get; set; } = default;

        public double NDemand { get; set; } = default;

        public string potentialDMAllocation { get; set; } = default;
    }
}
