using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class AccumulateFunction : ApsimNode
    {
        public string StartStageName { get; set; } = default;

        public string EndStageName { get; set; } = default;

        public string ResetStageName { get; set; } = default;

        public double FractionRemovedOnCut { get; set; } = default;

        public double FractionRemovedOnHarvest { get; set; } = default;

        public double FractionRemovedOnGraze { get; set; } = default;

        public double FractionRemovedOnPrune { get; set; } = default;

        public AccumulateFunction()
        { }
    }
}
