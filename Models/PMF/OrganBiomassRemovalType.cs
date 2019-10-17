using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF
{
    public class OrganBiomassRemovalType : ApsimNode
    {
        public double FractionLiveToRemove { get; set; } = default;

        public double FractionDeadToRemove { get; set; } = default;

        public double FractionLiveToResidue { get; set; } = default;

        public double FractionDeadToResidue { get; set; } = default;


    }
}
