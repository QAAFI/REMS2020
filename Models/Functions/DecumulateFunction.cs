using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class DecumulateFunction : Node
    {
        public double InitialValue { get; set; } = default;

        public double MinValue { get; set; } = default;

        public string StartStageName { get; set; } = default;

        public string ResetStageName { get; set; } = default;

        public string StopStageName { get; set; } = default;

        public DecumulateFunction()
        { }
    }
}
