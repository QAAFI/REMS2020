using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF.Struct
{
    public class CulmStructure : ApsimNode
    {
        public double FinalLeafNo { get; set; } = default;

        public double FertileTillerNumber { get; set; } = default;

        public double CurrentLeafNo { get; set; } = default;

        public string LeafInitialisationStage { get; set; } = default;

        public CulmStructure()
        { }
    }
}
